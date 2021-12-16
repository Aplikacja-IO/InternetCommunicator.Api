using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternetCommunicator.Api.Services;
using InternetCommunicator.Infrastructure.Context;

namespace ComponentTest1
{
    [TestClass]
    public class UnitTest1
    {
        CommunicatorDbContext _context;
        public UnitTest1()
        {
        }
        [TestMethod]
        public void CreateComponentTestMethod()
        {

            string postText = "new post";
            int authorId = 2;
            int parentGroupId = 1;


        }
    }
}
