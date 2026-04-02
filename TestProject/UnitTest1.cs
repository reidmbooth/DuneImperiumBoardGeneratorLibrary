using System.Security.Cryptography;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void RandomPartitionInt_TestIfOutputSumsCorrectly100Times()
        {
            Random rand = new Random();
            for (int k = 5; k < 105; k++)
            {
                int sum = 0;
                List<int> sum_list = DIGeneratorLibrary.ItemPoolBuilder.RandomPartitionInt(k, 5, rand);
                foreach(int i in sum_list)
                {
                    sum += i;
                }
                Assert.Equal(k, sum);
            }
        }

        [Fact]
        public void RandomPartitionInt_TestIfOutputSplitsCorrectly100Times()
        {
            Random rand = new Random();
            for (int k = 5; k < 105; k++)
            {
                List<int> sum_list = DIGeneratorLibrary.ItemPoolBuilder.RandomPartitionInt(k, 5, rand);
                Assert.Equal(5, sum_list.Count);
            }
        }


    }
}
