namespace Validator.Test.PredicateBuilderExtensionsTest.Strings;

using NUnit.Framework;
using Validator.Test.PredicateBuilderExtensionsTest;

public class NotEmpty
{
    [TestCase("", false, Description = "empty")]
    [TestCase(" ", false, Description = "whitespace")]
    [TestCase(null, false, Description = "null")]
    [TestCase("abc", true, Description = "a valid value")]
    public void Test(string value, bool expected)
    {
        Assert.That(Helper.Validate<NotEmptyValidator, string>(value).Valid == expected);
    }

    public class NotEmptyValidator : Validator<TestModel<string>>
    {
        public NotEmptyValidator()
        {
            For(model => model.Value).NotEmpty();
        }
    }
}
