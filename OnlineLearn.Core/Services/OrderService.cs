using Microsoft.EntityFrameworkCore;
using OnlineLearn.Core.DTOs.Order;
using OnlineLearn.Core.Services.Interfaces;
using OnlineLearn.DataLayer.Context;
using OnlineLearn.DataLayer.Entities.Course;
using OnlineLearn.DataLayer.Entities.Order;
using OnlineLearn.DataLayer.Entities.User;
using OnlineLearn.DataLayer.Entities.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OnlineLearn.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly OnlineLearnContext _context;
        private readonly IUserService _userService;

        public OrderService(OnlineLearnContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public void AddDiscount(Discount discount)
        {
            _context.Discounts.Add(discount);
            _context.SaveChanges();
        }

        public int AddOrder(string username, int courseId)
        {
            var userId = _userService.GetUserIdByUserName(username);

            Order order = _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinalized);
            var course = _context.Courses.Find(courseId);

            if (order == null)
            {
                order = new Order()
                {
                    UserId = userId,
                    IsFinalized = false,
                    CreateDate = DateTime.Now,
                    OrderSum = course.CoursePrice,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            CourseId=courseId,
                            Count=1,
                            Price=course.CoursePrice
                        }
                    }
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                OrderDetail detail = _context.OrderDetails
                    .FirstOrDefault(d => d.OrderId == order.OrderId && d.CourseId == courseId);
                if (detail != null)
                {
                    detail.Count += 1;
                    _context.OrderDetails.Update(detail);
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 1,
                        CourseId = courseId,
                        Price = course.CoursePrice
                    };
                    _context.OrderDetails.Add(detail);
                }

                _context.SaveChanges();
                UpdatePriceOrder(order.OrderId);
            }
            return order.OrderId;
        }

        public bool FinalOrder(string userName, int orderId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            var order = _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Course)
                .FirstOrDefault(o => o.UserId == userId && o.OrderId == orderId);
            if (order == null || order.IsFinalized)
            {
                return false;
            }

            if (_userService.UserWalletBalance(userName) >= order.OrderSum)
            {
                order.IsFinalized = true;
                _userService.AddWallet(new Wallet()
                {
                    Amount = order.OrderSum,
                    CreateDate = DateTime.Now,
                    Description = "فاکتور شماره #" + order.OrderId,
                    IsPay = true,
                    TypeId = 2,
                    UserId = userId
                });
                _context.Orders.Update(order);

                foreach (var detail in order.OrderDetails)
                {
                    _context.UserCourses.Add(new UserCourse()
                    {
                        CourseId = detail.CourseId,
                        UserId = userId
                    });
                }
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public List<Discount> GetAllDiscounts()
        {
            return _context.Discounts.ToList();
        }

        public Discount GetDiscountById(int discountId)
        {
            return _context.Discounts.Find(discountId);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public Order GetOrderForUserPanel(string userName, int orderId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Course)
                .FirstOrDefault(o => o.UserId == userId && o.OrderId == orderId);
        }

        public List<Order> GetUserOrders(string userName)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public bool IsExistCode(string code)
        {
            return _context.Discounts.Any(c => c.DiscountCode == code);
        }

        public bool IsUserInCourse(string userName, int courseId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.UserCourses.Any(u => u.UserId == userId && u.CourseId == courseId);
        }

        public void UpdateDiscount(Discount discount)
        {
            _context.Discounts.Update(discount);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void UpdatePriceOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            order.OrderSum = _context.OrderDetails.Where(d => d.OrderId == orderId).Sum(d => d.Price * d.Count);
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public DiscountUseType UseDiscount(int orderId, string code)
        {
            var discount = _context.Discounts.SingleOrDefault(d => d.DiscountCode == code);
            if (discount == null)
                return DiscountUseType.NotFound;

            if (discount.StartDate != null && discount.StartDate < DateTime.Now)
                return DiscountUseType.ExpiredDate;

            if (discount.EndDate != null && discount.EndDate >= DateTime.Now)
                return DiscountUseType.ExpiredDate;

            if (discount.UsableCount != null && discount.UsableCount < 1)
                return DiscountUseType.Finished;

            var order = GetOrderById(orderId);

            if (_context.UserDiscountCodes.Any(d => d.UserId == order.UserId && d.DiscountId == discount.DiscountId))
                return DiscountUseType.Used;

            int percent = (order.OrderSum * discount.DiscountPercent) / 100;
            order.OrderSum = order.OrderSum - percent;

            UpdateOrder(order);

            _context.UserDiscountCodes.Add(new UserDiscountCode()
            {
                UserId = order.UserId,
                DiscountId = discount.DiscountId
            });

            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
            }

            _context.Discounts.Update(discount);
            _context.SaveChanges();

            return DiscountUseType.Success;
        }
    }
}
