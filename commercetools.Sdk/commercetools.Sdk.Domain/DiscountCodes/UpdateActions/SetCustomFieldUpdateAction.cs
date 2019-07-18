﻿using System.ComponentModel.DataAnnotations;

namespace commercetools.Sdk.Domain.DiscountCodes.UpdateActions
{
    public class SetCustomFieldUpdateAction : UpdateAction<DiscountCode>
    {
        public string Action => "setCustomField";
        [Required]
        public string Name { get; set; }
        public object Value { get; set; }
    }
}