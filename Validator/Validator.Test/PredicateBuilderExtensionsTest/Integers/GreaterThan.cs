﻿namespace Validator.Test.PredicateBuilderExtensionsTest.Integers;

using NUnit.Framework;
using Validator.Test;

public class GreaterThan
{
    [TestCase(11, true)]
    [TestCase(10, false)]
    [TestCase(9, false)]
    [TestCase(-1, false)]
    [TestCase(int.MaxValue, true)]
    [TestCase(int.MinValue, false)]
    public void Test(int value, bool expected)
    {
        Assert.That(Helper.Validate<GreaterThanValidator, int>(value).Valid == expected);
    }

    public class GreaterThanValidator : Validator<TestModel<int>>
    {
        public GreaterThanValidator()
        {
            For(model => model.Value).GreaterThan(10);
        }
    }
}
