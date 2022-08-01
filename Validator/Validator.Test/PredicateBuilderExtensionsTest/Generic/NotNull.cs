namespace Validator.Test.PredicateBuilderExtensionsTest.Generic;

using NUnit.Framework;

public class NotNull
{
    [TestCase(false)]
    [TestCase(true)]
    public void Test(bool expected)
    {
        var value = expected ? new object() : null;
        Assert.That(Helper.Validate<NotNullValidator, object>(value).Valid == expected);
    }

    public class NotNullValidator : Validator<TestModel<object>>
    {
        public NotNullValidator()
        {
            For(model => model.Value).NotNull();
        }
    }
}