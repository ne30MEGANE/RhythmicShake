/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FancyScrollView
{
    class ScrollView : FancyScrollView<MusicData>
    {
        [SerializeField] Scroller scroller = default;
        [SerializeField] GameObject cellPrefab = default;

        protected override GameObject CellPrefab => cellPrefab;

        public static List<MusicData> MusicList;

        protected override void Initialize()
        {
            base.Initialize();
            scroller.OnValueChanged(UpdatePosition);
        }

        public void UpdateData(IList<MusicData> items)
        {
            UpdateContents(items);
            scroller.SetTotalCount(items.Count); // データ数セット
        }

        private void Awake() // Startより前
        {
            // 表示用データを格納
            TextAsset csv = Resources.Load("MusicList") as TextAsset;
            StringReader reader = new StringReader(csv.text);
            MusicList = new List<MusicData>();
            for(int i = 0; reader.Peek() > -1; i++){
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                MusicList.Add(toMusicData(values));
            }

            this.UpdateData(MusicList);
        }

        private MusicData toMusicData(string[] v){ // id, title, artist, level1, level2
            int e = int.Parse(v[3]);
            int n = int.Parse(v[4]);
            
            return new MusicData(v[0], v[1], v[2], e, n);
        }
    }
}
