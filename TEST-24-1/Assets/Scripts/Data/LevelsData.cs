using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using UImanager;

namespace Data
{
    [System.Serializable]
    public class LevelsData
    {
        // Костыль (ограничение на 100 лвлов)
        private int _lvlCount = 100;
        public bool[] Levels;
        private int _volumeCount = 3;
        public float[] Volumes;

        public LevelsData()
        {
            ResetAllLevels();
            Volumes = new float[_volumeCount];
            for (int i = 0; i < _volumeCount; i++)
            {
                Volumes[i] = 1;
            }
        }

        public void UnlockLevel(int lvl)
        {
            Levels[lvl] = true;
        }

        public void UnlockAllLevels()
        {
            for (int i = 0; i < LevelsManager.NumLevels; i++)
            {
                Levels[i] = true;
            }
        }

        public void ResetAllLevels()
        {
            Levels = new bool[_lvlCount];
            Levels[0] = true;
        }

        public void SetVolume(int mixer, float volume)
        {
            Volumes[mixer] = volume;
        }

        public float GetVolume(int mixer)
        {
            return Volumes[mixer];
        }
    }
}
