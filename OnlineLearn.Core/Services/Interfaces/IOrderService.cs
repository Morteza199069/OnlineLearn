using OnlineLearn.DataLayer.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;


namespace OnlineLearn.Core.Services.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(string username, int courseId);
        void UpdatePriceOrder(int orderId);
        Order GetOrderForUserPanel(string userName, int orderId);
        bool FinalOrder(string userName, int orderId);



    }
}
