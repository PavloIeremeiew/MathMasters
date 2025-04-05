using Cysharp.Threading.Tasks;
using MathMasters.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using Zenject;

namespace MathMasters
{
    public class BlockMenu : MonoBehaviour
    {
        [SerializeField] private int _blockId = 0;
        [SerializeField] private Block _block;
        [SerializeField] private LevelButton[] _buttons;
        [SerializeField] private TextMeshProUGUI _title;
        private UnityAction<Level, LevelInfo> _activeAction;
        private UnityAction<Level, LevelInfo> _doneAction;
        public int Id => _blockId;

        [Inject] 
        public void Init(ILoadRemoteAddressables loadRemote)
        {
            loadRemote.LoadAsset<Block>($"Block{_blockId+1}", SetBlock);
        }
        private void SetBlock(Block block)
        {
            _block = block;
        }

        private void Start()
        {
            _title.text = $"BLOCK {Id+1}";
        }

        public async void SetDoneAll()
        {
            await UniTask.WaitUntil(() => _block != null);
            for (int i=0; i< _buttons.Length;i++)
            {
                SetDone(i);
            }
        }
        public async void SetLockAll()
        {
            await UniTask.WaitUntil(() => _block != null);
            foreach (LevelButton button in _buttons)
            {
                button.Lock();
            }
        }
        public async void SetActive(int id, UnityAction<Level, LevelInfo> activeAction, UnityAction<Level, LevelInfo> doneAction)
        {
            await UniTask.WaitUntil(() => _block != null);
            _activeAction = activeAction;
            _doneAction = doneAction;

            if (id >= _buttons.Length)
            {
                SetDoneAll();
                return;
            }
            if (id < 0)
            {
                SetLockAll();
                return;
            }
            SetCurrentActive(id, activeAction);
        }

        private void SetCurrentActive(int id, UnityAction<Level, LevelInfo> action)
        {
            SetBeforId(id);
            SetCurrent(id, action);
            SetAfterId(id);
        }
        private void SetCurrent(int id, UnityAction<Level, LevelInfo> action)
        {
            if (_block.Levels.Length > id)
            {
                SetAction(id);
            }
            else
            {
                if (_buttons.Length > id)
                {
                    _buttons[id].Lock();
                }
            }

        }
        private void SetAction(int id)
        {
            Level level = _block.Levels[id];
            LevelInfo info = new LevelInfo()
            {
                Block = Id,
                Level = id
            };
            _buttons[id].Active(() => _activeAction(level, info));
        }
        private void SetDone(int id)
        {
            Level level = _block.Levels[id];
            LevelInfo info = new LevelInfo()
            {
                Block = Id,
                Level = id
            };
            _buttons[id].Done(() => _doneAction(level, info));
        }
        private void SetBeforId(int id)
        {
            for (int i = 0; i < id; i++)
            {
                SetDone(i);
            }
        }
        private void SetAfterId(int id)
        {
            for (int i = id + 1; i < _buttons.Length; i++)
            {
                _buttons[i].Lock();
            }
        }
        public void StartAnim(int id)
        {
            UnityAction action = null;
            if(id < _buttons.Length)
            {
                _buttons[id].BeforUnLockAnim();
                action = _buttons[id].UnLockAnim;
            }
            _buttons[id - 1].DoneAnim(action);
        }
    }
}
