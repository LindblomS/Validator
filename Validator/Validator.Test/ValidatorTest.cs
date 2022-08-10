namespace Validator.Test;

using Validator;
using NUnit.Framework;
using Validator.Models;
using Validator.Resources;

public static class ValidatorTestExtensions
{
    public static bool ShouldHaveFailureForProperty(this Result result, string propertyName)
    {
        return result.Failures.FirstOrDefault(failure => failure.PropertyName == propertyName) != default;
    }
}

public class ValidatorTest
{
    public class BaseTestValidator : Validator<TestModel<string>>
    {

    }

    public abstract class Common<TValidator> where TValidator : BaseTestValidator, new()
    {
        public static Result Validate(string value)
        {
            return Helper.Validate<TValidator, string>(value);
        }

        public static Result Validate(TestModel<string> model)
        {
            return Helper.Validate<TValidator, string>(model);
        }
    }

    [Test]
    public void should_not_return_any_failures_when_model_is_null()
    {
        Assert.That(Helper.Validate<BaseTestValidator, string>(default(TestModel<string>)).Valid, Is.True);
    }

    [Test]
    public void should_throw_InvalidOperationException_when_no_validators_are_found()
    {
        Assert.Throws<InvalidOperationException>(() => Helper.Validate<BaseTestValidator, string>("a"));
    }

    public class with_one_predicate
    {
        public class with_no_message : Common<with_no_message.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_predicate_pass()
            {
                Assert.That(Validate("asdf").Failures, Is.Empty);
            }

            [Test]
            public void should_have_failure_for_property_when_predicate_fail()
            {
                Assert.That(Validate("").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
            }

            [Test]
            public void should_have_one_failure_when_predicate_fail()
            {
                Assert.That(Validate("").Failures, Has.Count.EqualTo(1));
            }

            public class TestValidator : BaseTestValidator
            {
                public TestValidator()
                {
                    For(model => model.Value).NotEmpty();
                }
            }
        }

        public class with_message : Common<with_message.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_predicate_pass()
            {
                Assert.That(Validate("asdf").Failures, Is.Empty);
            }

            [Test]
            public void should_have_failure_for_property_when_predicate_fail()
            {
                Assert.That(Validate("").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
            }

            [Test]
            public void should_have_one_failure_when_predicate_fail()
            {
                Assert.That(Validate("").Failures, Has.Count.EqualTo(1));
            }

            [Test]
            public void should_have_failure_with_message_when_predicate_fail()
            {
                Assert.That(Validate("").Failures.Single().Message == "message");
            }

            public class TestValidator : BaseTestValidator
            {
                public TestValidator()
                {
                    For(model => model.Value).NotEmpty().WithMessage("message");
                }
            }
        }

        public class with_one_condition
        {
            public class with_no_message : Common<with_no_message.TestValidator>
            {
                [Test]
                public void should_not_have_any_failures_when_condition_fail()
                {
                    Assert.That(Validate(default(TestModel<string>)).Failures, Is.Empty);
                }

                [Test]
                public void should_not_have_any_failures_when_condition_and_predicate_pass()
                {
                    Assert.That(Validate("asdf").Failures, Is.Empty);
                }

                [Test]
                public void should_have_one_failure_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate("").Failures, Has.Count.EqualTo(1));
                }

                [Test]
                public void should_have_failure_for_property_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate("").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
                }

                public class TestValidator : BaseTestValidator
                {
                    public TestValidator()
                    {
                        For(model => model.Value).If(model => model is not null).NotEmpty();
                    }
                }
            }

            public class with_message : Common<with_message.TestValidator>
            {
                [Test]
                public void should_not_have_any_failures_when_condition_fail()
                {
                    Assert.That(Validate(default(TestModel<string>)).Failures, Is.Empty);
                }

                [Test]
                public void should_not_have_any_failures_when_condition_and_predicate_pass()
                {
                    Assert.That(Validate("asdf").Failures, Is.Empty);
                }

                [Test]
                public void should_have_one_failure_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate("").Failures, Has.Count.EqualTo(1));
                }

                [Test]
                public void should_have_failure_for_property_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate("").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
                }

                [Test]
                public void should_have_failure_with_message_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate("").Failures.Single().Message == "message");
                }

                public class TestValidator : BaseTestValidator
                {
                    public TestValidator()
                    {
                        For(model => model.Value).If(model => model is not null).NotEmpty().WithMessage("message");
                    }
                }
            }
        }
    }

    public class with_multiple_chained_predicates
    {
        public class with_no_messages : Common<with_no_messages.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_predicates_pass()
            {
                Assert.That(Validate("c").Failures, Is.Empty);
            }

            [Test]
            public void should_only_have_failure_for_first_predicate_when_both_fail()
            {
                Assert.That(Validate("abc").Failures.Single().Message == MessageManager.NotEquals("abc"));
            }

            [Test]
            public void should_have_failure_for_second_predicate_when_first_pass_and_second_fail()
            {
                Assert.That(Validate("cba").Failures.Single().Message == null);
            }

            [Test]
            public void should_have_failure_for_property_when_first_predicate_fail()
            {
                Assert.That(Validate("abc").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
            }

            [Test]
            public void should_have_failure_for_property_when_first_predicate_pass_and_second_fail()
            {
                Assert.That(Validate("cba").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
            }

            public class TestValidator : BaseTestValidator
            {
                public TestValidator()
                {
                    For(model => model.Value)
                        .NotEquals("abc")
                        .Custom(value => !value.Contains("b"));
                }
            }
        }

        public class with_messages : Common<with_messages.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_predicates_pass()
            {
                Assert.That(Validate("c").Failures, Is.Empty);
            }

            [Test]
            public void should_have_failure_for_property_when_first_predicate_fail()
            {
                Assert.That(Validate("abc").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
            }

            [Test]
            public void should_have_failure_for_property_when_first_predicate_pass_and_second_fail()
            {
                Assert.That(Validate("cba").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
            }

            [Test]
            public void should_have_failure_with_message_from_first_predicate_when_first_predicate_fail()
            {
                Assert.That(Validate("abc").Failures.Single().Message == "message1");
            }

            [Test]
            public void should_have_failure_with_message_from_second_predicate_when_second_predicate_fail()
            {
                Assert.That(Validate("cba").Failures.Single().Message == "message2");
            }

            public class TestValidator : BaseTestValidator
            {
                public TestValidator()
                {
                    For(model => model.Value)
                        .NotEquals("abc")
                        .WithMessage("message1")
                        .Custom(value => !value.Contains("b"))
                        .WithMessage("message2");
                }
            }
        }

        public class with_one_condition
        {
            public class before : Common<before.TestValidator>
            {
                [Test]
                public void should_not_have_any_failures_when_condition_fail()
                {
                    Assert.That(Validate(default(TestModel<string>)).Failures.Any(), Is.False);
                }

                public class when_condition_pass : before
                {
                    [Test]
                    public void should_not_have_any_failures_when_predicates_pass()
                    {
                        Assert.That(Validate("c").Failures, Is.Empty);
                    }

                    [Test]
                    public void should_only_have_failure_for_first_predicate_when_both_fail()
                    {
                        Assert.That(Validate("abc").Failures.Single().Message == MessageManager.NotEquals("abc"));
                    }

                    [Test]
                    public void should_have_failure_for_second_predicate_when_first_pass_and_second_fail()
                    {
                        Assert.That(Validate("cba").Failures.Single().Message == null);
                    }

                    [Test]
                    public void should_have_failure_for_property_when_first_predicate_fail()
                    {
                        Assert.That(Validate("abc").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
                    }

                    [Test]
                    public void should_have_failure_for_property_when_first_predicate_pass_and_second_fail()
                    {
                        Assert.That(Validate("cba").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
                    }
                }

                public class TestValidator : BaseTestValidator
                {
                    public TestValidator()
                    {
                        For(model => model.Value)
                            .If(model => model is not null)
                            .NotEquals("abc")
                            .Custom(value => !value.Contains("b"));
                    }
                }
            }

            public class in_middle : Common<in_middle.TestValidator>
            {
                [Test]
                public void should_not_have_any_failures_when_condition_fail_and_second_predicate_fail()
                {
                    Assert.That(Validate("cbab").Failures, Is.Empty);
                }

                public class when_condition_pass : in_middle
                {
                    [Test]
                    public void should_not_have_any_failures_when_predicates_pass()
                    {
                        Assert.That(Validate("c").Failures, Is.Empty);
                    }

                    [Test]
                    public void should_only_have_failure_for_first_predicate_when_both_fail()
                    {
                        Assert.That(Validate("abc").Failures.Single().Message == MessageManager.NotEquals("abc"));
                    }

                    [Test]
                    public void should_have_failure_for_second_predicate_when_first_pass_and_second_fail()
                    {
                        Assert.That(Validate("cba").Failures.Single().Message == null);
                    }

                    [Test]
                    public void should_have_failure_for_property_when_first_predicate_fail()
                    {
                        Assert.That(Validate("abc").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
                    }

                    [Test]
                    public void should_have_failure_for_property_when_first_predicate_pass_and_second_fail()
                    {
                        Assert.That(Validate("cba").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
                    }
                }

                public class TestValidator : BaseTestValidator
                {
                    public TestValidator()
                    {
                        For(model => model.Value)
                            .NotEquals("abc")
                            .If(model => model.Value != "cbab")
                            .Custom(value => !value.Contains("b"));
                    }
                }
            }
        }
    }

    public class with_multiple_non_chained_predicates : Common<with_multiple_non_chained_predicates.TestValidator>
    {
        [Test]
        public void should_not_have_any_failures_when_both_predicates_pass()
        {
            Assert.That(Validate("").Failures, Is.Empty);
        }

        [Test]
        public void should_have_two_failures_when_both_predicates_fail()
        {
            Assert.That(Validate("ab").Failures, Has.Count.EqualTo(2));
        }

        [Test]
        public void should_have_one_failure_when_first_predicate_pass_and_second_fail()
        {
            Assert.That(Validate("b").Failures, Has.Count.EqualTo(1));
        }

        [Test]
        public void should_have_one_failure_when_first_predicate_fail_and_second_pass()
        {
            Assert.That(Validate("a").Failures, Has.Count.EqualTo(1));
        }

        [Test]
        public void should_have_failure_for_property_when_first_predicate_fail_and_second_pass()
        {
            Assert.That(Validate("a").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
        }

        [Test]
        public void should_have_failure_for_property_when_first_predicate_pass_and_second_fail()
        {
            Assert.That(Validate("b").ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
        }

        public class TestValidator : BaseTestValidator
        {
            public TestValidator()
            {
                For(model => model.Value).Custom(value => !value.Contains('a'));
                For(model => model.Value).Custom(value => !value.Contains('b'));
            }
        }
    }

    public class with_nested_validator
    {
        public static TestModel<string> CreateWithNestedModel(string value)
        {
            return new TestModel<string>(new TestModel<string>(value));
        }

        public class with_no_condition : Common<with_no_condition.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_nested_validator_pass()
            {
                Assert.That(Validate(CreateWithNestedModel("a")).Failures, Is.Empty);
            }

            [Test]
            public void should_have_one_failure_when_nested_validator_fail()
            {
                Assert.That(Validate(CreateWithNestedModel("")).Failures, Has.Count.EqualTo(1));
            }

            [Test]
            public void should_have_failure_for_property_when_nested_validator_fail()
            {
                Assert.That(Validate(CreateWithNestedModel(""))
                    .ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
            }

            public class TestValidator : BaseTestValidator
            {
                public TestValidator()
                {
                    For(model => model.NestedModel).Set(new NestedValidator());
                }
            }
        }

        public class with_condition : Common<with_condition.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_condition_fail()
            {
                Assert.That(Validate(default(TestModel<string>)).Failures, Is.Empty);
            }

            public class when_condition_pass : with_condition
            {
                [Test]
                public void should_not_have_any_failures_when_nested_validator_pass()
                {
                    Assert.That(Validate(CreateWithNestedModel("a")).Failures, Is.Empty);
                }

                [Test]
                public void should_have_one_failure_when_nested_validator_fail()
                {
                    Assert.That(Validate(CreateWithNestedModel("")).Failures, Has.Count.EqualTo(1));
                }

                [Test]
                public void should_have_failure_for_property_when_nested_validator_fail()
                {
                    Assert.That(Validate(CreateWithNestedModel(""))
                        .ShouldHaveFailureForProperty(nameof(TestModel<string>.Value)));
                }
            }

            public class TestValidator : BaseTestValidator
            {
                public TestValidator()
                {
                    For(model => model.NestedModel)
                        .If(model => model is not null)
                        .Set(new NestedValidator());
                }
            }
        }

        public class NestedValidator : BaseTestValidator
        {
            public NestedValidator()
            {
                For(model => model.Value).NotEmpty();
            }
        }
    }
}


