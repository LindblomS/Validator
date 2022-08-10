namespace Validator.Test.PredicateBuilderExtensionsTest.Generic;

using NUnit.Framework;

public class NotEqualsTest
{
    [TestCase(true)]
    [TestCase(false)]
    public void Test(bool expected)
    {
        var value = new Dummy();
        var thing = expected ? new Dummy() : value;
        Assert.That(Helper.Validate<NotEqualsValidator, Dummy>(value, thing).Valid == expected);
    }

    public class NotEqualsValidator : Validator<TestModel<Dummy>>
    {
        public NotEqualsValidator(Dummy o)
        {
            For(model => model.Value).NotEquals(o);
        }
    }

    public class Dummy
    {
    }
}
