using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class StackTests
    {

        [Test]
        public void Push_ArgumentIsNull_ThrowArgumentNullException()
        {
            var stack = new Stack<string>();
            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }
        
        [Test]
        public void Push_ValidArgument_AddTheObjectToTheStack()
        {
            var stack = new Stack<string>();
            
            stack.Push("New String");

            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            var stack = new Stack<string>();
            
            Assert.That(stack.Count, Is.EqualTo(0));
            
        }
        
        [Test]
        public void Pop_StackWithAFewObjects_RemoveTheObjectOnTheTop()
        {
            var stack = new Stack<string>();
            stack.Push("New String 1");
            stack.Push("New String 2");
            stack.Push("New String 3");

            stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Pop_StackWithAFewObjects_ReturnTheObjectOnTheTop()
        {
            var stack = new Stack<string>();
            stack.Push("New String 1");
            stack.Push("New String 2");
            stack.Push("New String 3");

            var result = stack.Pop();

            Assert.That(result, Is.EqualTo("New String 3"));
        }

        [Test]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }
        
        [Test]
        public void Peek_Empty_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }
        
        [Test]
        public void Peek_WhenCalled_ReturnTheObjectOnTopOfTheStack()
        {
            var stack = new Stack<string>();
            stack.Push("New String 1");
            stack.Push("New String 2");

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo("New String 2"));
        }
        
        [Test]
        public void Peek_WhenCalled_DoesNotRemoveTheObjectOnTopOfTheStack()
        {
            var stack = new Stack<string>();
            stack.Push("New String 1");
            stack.Push("New String 2");

            stack.Peek();

            Assert.That(stack.Count, Is.EqualTo(2));
        }
    }
}