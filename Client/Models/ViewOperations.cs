using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models
{
    public class ViewOperations
    {
        public static bool isDisabledStatusEdit(OrderStatus status)
        {
            if (status.Name.Equals("Canceled") || status.Name.Equals("Aborted") || status.Name.Equals("Ended"))
                return true;
            else
                return false;
        }
    }
}