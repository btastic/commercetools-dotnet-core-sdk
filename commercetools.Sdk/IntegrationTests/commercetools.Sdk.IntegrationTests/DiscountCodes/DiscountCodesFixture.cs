using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.CartDiscounts;
using commercetools.Sdk.Domain.DiscountCodes;
using static commercetools.Sdk.IntegrationTests.GenericFixture;
using static commercetools.Sdk.IntegrationTests.CartDiscounts.CartDiscountsFixture;
using Type = commercetools.Sdk.Domain.Types.Type;

namespace commercetools.Sdk.IntegrationTests.DiscountCodes
{
    public static class DiscountCodesFixture
    {
        #region DraftBuilds

        public static DiscountCodeDraft DefaultDiscountCodeDraft(DiscountCodeDraft discountCodeDraft)
        {
            var random = TestingUtility.RandomInt();
            discountCodeDraft.Name = new LocalizedString {{"en", $"DiscountCode_{random}"}};
            discountCodeDraft.Code = TestingUtility.RandomString();
            discountCodeDraft.IsActive = true;
            discountCodeDraft.CartPredicate = "1 = 1"; //for all carts
            discountCodeDraft.ValidFrom = DateTime.Today;
            discountCodeDraft.ValidUntil = DateTime.Today.AddDays(10); // valid 10 days
            return discountCodeDraft;
        }

        public static DiscountCodeDraft DefaultDiscountCodeDraftWithCartDiscounts(DiscountCodeDraft draft,
            List<Reference<CartDiscount>> cartDiscounts)
        {
            var discountCodeDraft = DefaultDiscountCodeDraft(draft);
            discountCodeDraft.CartDiscounts = cartDiscounts;
            return discountCodeDraft;
        }

        public static DiscountCodeDraft DefaultDiscountCodeDraftWithCode(DiscountCodeDraft draft,
            string code)
        {
            var discountCodeDraft = DefaultDiscountCodeDraft(draft);
            discountCodeDraft.Code = code;
            return discountCodeDraft;
        }

        public static DiscountCodeDraft DefaultDiscountCodeDraftWithCustomType(DiscountCodeDraft draft,
            Type type, Fields fields)
        {
            var customFieldsDraft = new CustomFieldsDraft
            {
                Type = type.ToKeyResourceIdentifier(),
                Fields = fields
            };

            var discountCodeDraft = DefaultDiscountCodeDraft(draft);
            discountCodeDraft.Custom = customFieldsDraft;

            return discountCodeDraft;
        }

        #endregion

        #region WithDiscountCode

        public static async Task WithDiscountCode(IClient client, Action<DiscountCode> func)
        {
            await WithCartDiscount(client, async cartDiscount =>
            {
                var cartDiscounts = new List<Reference<CartDiscount>>()
                {
                    new Reference<CartDiscount>
                    {
                        Id = cartDiscount.Id
                    }
                };
                await With(client, new DiscountCodeDraft(),
                    discountCodeDraft => DefaultDiscountCodeDraftWithCartDiscounts(discountCodeDraft, cartDiscounts),
                    func);
            });
        }

        public static async Task WithDiscountCode(IClient client,
            Func<DiscountCodeDraft, DiscountCodeDraft> draftAction, Action<DiscountCode> func)
        {
            await WithCartDiscount(client, async cartDiscount =>
            {
                var cartDiscounts = new List<Reference<CartDiscount>>()
                {
                    new Reference<CartDiscount>
                    {
                        Id = cartDiscount.Id
                    }
                };
                await With(client, DefaultDiscountCodeDraftWithCartDiscounts(
                        new DiscountCodeDraft(), cartDiscounts),
                    draftAction, func);
            });
        }

        public static async Task WithDiscountCode(IClient client, Func<DiscountCode, Task> func)
        {
            await WithCartDiscount(client, DefaultCartDiscountDraftRequireDiscountCode, async cartDiscount =>
            {
                var cartDiscounts = new List<Reference<CartDiscount>>()
                {
                    new Reference<CartDiscount> {Id = cartDiscount.Id}
                };
                await WithAsync(client, new DiscountCodeDraft(),
                    discountCodeDraft => DefaultDiscountCodeDraftWithCartDiscounts(discountCodeDraft, cartDiscounts),
                    func);
            });
        }

        public static async Task WithDiscountCode(IClient client,
            Func<DiscountCodeDraft, DiscountCodeDraft> draftAction, Func<DiscountCode, Task> func)
        {
            await WithCartDiscount(client, DefaultCartDiscountDraftRequireDiscountCode, async cartDiscount =>
            {
                var cartDiscounts = new List<Reference<CartDiscount>>()
                {
                    new Reference<CartDiscount> {Id = cartDiscount.Id}
                };
                await WithAsync(client,
                    DefaultDiscountCodeDraftWithCartDiscounts(new DiscountCodeDraft(), cartDiscounts),
                    draftAction, func);
            });
        }

        #endregion

        #region WithUpdateableDiscountCode

        public static async Task WithUpdateableDiscountCode(IClient client, Func<DiscountCode, DiscountCode> func)
        {
            await WithCartDiscount(client, DefaultCartDiscountDraftRequireDiscountCode, async cartDiscount =>
            {
                var cartDiscounts = new List<Reference<CartDiscount>>()
                {
                    new Reference<CartDiscount> {Id = cartDiscount.Id}
                };
                await WithUpdateable(client, new DiscountCodeDraft(),
                    discountCodeDraft => DefaultDiscountCodeDraftWithCartDiscounts(discountCodeDraft, cartDiscounts),
                    func);
            });
        }

        public static async Task WithUpdateableDiscountCode(IClient client,
            Func<DiscountCodeDraft, DiscountCodeDraft> draftAction, Func<DiscountCode, DiscountCode> func)
        {
            await WithUpdateable(client, new DiscountCodeDraft(), draftAction, func);
        }

        public static async Task WithUpdateableDiscountCode(IClient client, Func<DiscountCode, Task<DiscountCode>> func)
        {
            await WithCartDiscount(client, DefaultCartDiscountDraftRequireDiscountCode, async cartDiscount =>
            {
                var cartDiscounts = new List<Reference<CartDiscount>>()
                {
                    new Reference<CartDiscount> {Id = cartDiscount.Id}
                };
                await WithUpdateableAsync(client, new DiscountCodeDraft(),
                    discountCodeDraft => DefaultDiscountCodeDraftWithCartDiscounts(discountCodeDraft, cartDiscounts),
                    func);
            });
        }

        public static async Task WithUpdateableDiscountCodeWithCustomType(IClient client,
            Type type, Fields fields, Func<DiscountCode, Task<DiscountCode>> func)
        {
            await WithCartDiscount(client, DefaultCartDiscountDraftRequireDiscountCode, async cartDiscount =>
            {
                var cartDiscounts = new List<Reference<CartDiscount>>()
                {
                    new Reference<CartDiscount> {Id = cartDiscount.Id}
                };

                await WithUpdateableAsync(client,
                    DefaultDiscountCodeDraftWithCartDiscounts(new DiscountCodeDraft(), cartDiscounts),
                    discountCodeDraft =>
                        DefaultDiscountCodeDraftWithCustomType(discountCodeDraft, type, fields), func);
            });
        }

        #endregion
    }
}