﻿namespace commercetools.Sdk.Domain.ShoppingLists
{
    using System;

    public class LineItem
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int VariantId { get; set; }
        public Reference<ProductType> ProductType { get; set; }
        public long Quantity { get; set; }
        public CustomFields Custom { get; set; }
        public DateTime AddedAt { get; set; }
        public LocalizedString Name { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        public LocalizedString ProductSlug { get; set; }
        public ProductVariant Variant { get; set; }
    }
}
