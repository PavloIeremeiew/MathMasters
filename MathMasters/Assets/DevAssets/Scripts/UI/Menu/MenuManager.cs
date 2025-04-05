using MathMasters.Entities;
using MathMasters.Services;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace MathMasters
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] BlockMenu[] _blocks;
        [SerializeField] LevelSelectMenu _levelSelect;
        [SerializeField] TextMeshProUGUI _coins;
        [Inject] private LevelDataSignal _signal;
        [Inject] private BackToMenuDataSignal _dataSignal;
        [Inject] private SoundManager _soundManager;
        [Inject] private ISaver _saver;

        private void Start()
        {
            int blockId = _saver.GetBlock();
            int LevelId = _saver.GetLevel();
            _coins.text = _saver.GetMoney().ToString();
            foreach (BlockMenu block in _blocks) 
            {
                if (block.Id < blockId)
                {
                    block.SetDoneAll();
                }
                else
                {
                    if (block.Id > blockId)
                    {
                        block.SetLockAll();
                    }
                    else
                    {
                        block.SetActive(LevelId, SetNewLeveL, SetOldLeveL);
                        if (_dataSignal.IsUnlockNewLevel)
                        {
                            block.StartAnim(LevelId);
                        }
                    }
                }            
            }

            

        }
        private void SetNewLeveL(Level level, LevelInfo info)
        {
            _soundManager.PlayClickSound();
            _signal.CurrentLevel = level;
            _signal.IsReplay = false;
            int task = level.Questions.Length;
            int reward = level.Questions.Length;

            _levelSelect.Show(info, task, reward);
        }
        private void SetOldLeveL(Level level, LevelInfo info)
        {
            _soundManager.PlayClickSound();
            _signal.CurrentLevel = level;
            _signal.IsReplay = true;
            int task = level.Questions.Length;
            int reward =0;

            _levelSelect.Show(info, task, reward);
        }
    }
}
