﻿using commercetools.Sdk.Domain.Common;
using commercetools.Sdk.Domain.TaxCategories;

namespace commercetools.Sdk.Domain.Carts
{
    using System.ComponentModel.DataAnnotations;

    public class CustomLineItemDraft
    {
        [Required]
        public LocalizedString Name { get; set; }

        public long Quantity { get; set; }

        [Required]
        public BaseMoney Money { get; set; }

        [Required]
        public string Slug { get; set; }

        public IReference<TaxCategory> TaxCategory { get; set; }

        public ExternalTaxRateDraft ExternalTaxRate { get; set; }

        public CustomFieldsDraft Custom { get; set; }

        public ItemShippingDetails ShippingDetails { get; set; }
    }
}
