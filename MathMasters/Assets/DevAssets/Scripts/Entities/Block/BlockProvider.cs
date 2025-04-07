using Cysharp.Threading.Tasks;
using MathMasters.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MathMasters
{
    public class BlockProvider
    {
        private readonly Dictionary<int, Block> _blocks = new();
        private readonly Dictionary<int, UniTask<Block>> _loadingTasks = new();

        public UniTask<Block> Load(int blockId)
        {
            if (_blocks.TryGetValue(blockId, out var cached))
                return UniTask.FromResult(cached);

            if (_loadingTasks.TryGetValue(blockId, out var loadingTask))
                return loadingTask;

            UniTask<Block> task = Addressables.LoadAssetAsync<Block>($"Block{blockId + 1}")
                .ToUniTask()
                .ContinueWith(block => SetBlock(block, blockId));

            _loadingTasks[blockId] = task;
            return task;
        }

        private Block SetBlock(Block block, int blockId) {
            _blocks[blockId] = block;
            _loadingTasks.Remove(blockId);
            return block;
        }

    }
}
