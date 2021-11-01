﻿using OnlineLearn.Core.DTOs.Order;
using OnlineLearn.DataLayer.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;


namespace OnlineLearn.Core.Services.Interfaces
{
    public interface IOrderService
    {
        #region Order
        int AddOrder(string username, int courseId);
        void UpdatePriceOrder(int orderId);
        Order GetOrderForUserPanel(string userName, int orderId);
        Order GetOrderById(int orderId);
        bool FinalOrder(string userName, int orderId);
        List<Order> GetUserOrders(string userName);
        void UpdateOrder(Order order);
        #endregion

        #region Discount
        DiscountUseType UseDiscount(int orderId, string code);
        #endregion
    }
}