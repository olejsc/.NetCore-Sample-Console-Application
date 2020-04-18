using System;
using Transport;
using Xunit;


namespace TransportTests
{
    /*Fake - A fake is a generic term which can be used to describe either a stub or a mock object.
     * Whether it is a stub or a mock depends on the context in which it's used. So in other words, a fake can be a stub or a mock.

    Mock - A mock object is a fake object in the system that decides whether or not a unit test has passed or failed.
    A mock starts out as a Fake until it is asserted against.

    Stub - A stub is a controllable replacement for an existing dependency (or collaborator) in the system.
    By using a stub, you can test your code without dealing with the dependency directly. By default, a fake starts out as a stub.
    */
    public class ABusTests
    {
        [Fact]
        public void PassingTest ()
        {
            Assert.Equal(4, 2+2);
        }
    }
}
