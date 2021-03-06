﻿using System;
using NodaMoney;

namespace commercetools.Sdk.Domain
{
    public static class MoneyExtension
    {
        public static NodaMoney.Money ToNodaMoney(this Money money, MidpointRounding midpointRounding = MidpointRounding.ToEven)
        {
            return new NodaMoney.Money(money.AmountToDecimal(), money.CurrencyCode, midpointRounding);
        }

        public static NodaMoney.Money ToNodaMoney(this HighPrecisionMoney money, MidpointRounding midpointRounding = MidpointRounding.ToEven)
        {
            var builder = new CurrencyBuilder(money.CurrencyCode, "HighPrecision");
                builder.LoadDataFromCurrency(Currency.FromCode(money.CurrencyCode));
                builder.DecimalDigits = money.FractionDigits.GetValueOrDefault();

            return new NodaMoney.Money(money.AmountToDecimal(), builder.Build(), midpointRounding);
        }

        public static Money ToCtpMoney(this NodaMoney.Money money, MidpointRounding midpointRounding = MidpointRounding.ToEven)
        {
            return Money.FromDecimal(money.Currency.Code, money.Amount, midpointRounding);
        }

        public static HighPrecisionMoney ToCtpHighPrecisionMoney(this NodaMoney.Money money, MidpointRounding midpointRounding = MidpointRounding.ToEven)
        {
            return HighPrecisionMoney.FromDecimal(money.Currency.Code, money.Amount, (int)money.Currency.DecimalDigits, midpointRounding);
        }
    }
}
