namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void RandomPartitionInt_TestIfOutputSumsCorrectly100Times()
        {
            for (int k = 5; k < 105; k++)
            {
                int sum = 0;
                List<int> sum_list = DIGeneratorLibrary.ItemPoolBuilder.RandomPartitionInt(k, 5);
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
            for (int k = 5; k < 105; k++)
            {
                List<int> sum_list = DIGeneratorLibrary.ItemPoolBuilder.RandomPartitionInt(k, 5);
                Assert.Equal(5, sum_list.Count);
            }
        }

        [Fact]
        public void RandomPartitionInt_EdgeCase_x_equals_y()
        {
            // When x == y, each partition should be exactly 1
            List<int> result = DIGeneratorLibrary.ItemPoolBuilder.RandomPartitionInt(5, 5);
            Assert.Equal(5, result.Count);
            Assert.All(result, val => Assert.Equal(1, val));
        }

        [Fact]
        public void RandomPartitionInt_EdgeCase_SinglePartition()
        {
            // When split = 1, result should have 1 element equal to x
            List<int> result = DIGeneratorLibrary.ItemPoolBuilder.RandomPartitionInt(100, 1);
            Assert.Single(result);
            Assert.Equal(100, result[0]);
        }

        [Fact]
        public void RandomPartitionInt_ThrowsOnInvalidInput()
        {
            // x < y should throw
            Assert.Throws<ArgumentException>(() =>
                DIGeneratorLibrary.ItemPoolBuilder.RandomPartitionInt(3, 5));

            // y <= 0 should throw
            Assert.Throws<ArgumentException>(() =>
                DIGeneratorLibrary.ItemPoolBuilder.RandomPartitionInt(5, 0));
        }
    }
}
