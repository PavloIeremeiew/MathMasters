using Cysharp.Threading.Tasks;

namespace MathMasters.Services
{
    public interface ISaver
    {
        public void SaveMoney(int amount);
        public UniTask<int> GetMoney();

        public void SaveLevel(int number);
        public UniTask<int> GetLevel();

        public void SaveBlock(int number);
        public UniTask<int> GetBlock();
    }
}
