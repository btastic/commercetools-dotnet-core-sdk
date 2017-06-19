﻿using commercetools.Common;

namespace commercetools.CartDiscount
{
    public static class Extensions
    {
        /// <summary>
        /// Creates an instance of the CartDiscountManager.
        /// </summary>
        /// <returns>CartDiscountManager</returns>
        public static CartDiscountManager CartDiscounts(this Client client)
        {
            return new CartDiscountManager(client);
        }
    }
}
