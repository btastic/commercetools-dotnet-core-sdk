﻿namespace commercetools.Sdk.Domain.ShoppingLists.UpdateActions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ChangeLineItemOrderUpdateAction : UpdateAction<ShoppingList>
    {
        public string Action => "changeLineItemsOrder";
        [Required]
        public List<string> LineItemOrder { get; set; }
    }
}