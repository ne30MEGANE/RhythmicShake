/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System;
using UnityEngine;

namespace FancyScrollView
{
    public class MusicData // 個々の楽曲データ
    {
        public int ID { get; }
        public string MusicID { get; } // 内部タイトル
        public string Title { get; } // 表示タイトル
        public string Artist { get; }
        public int Easy { get; }
        public int Normal { get; }

        public MusicData(int id, string Mid, string title, string artist, int easy, int normal)
        {
            ID = id;
            MusicID = Mid;
            Title = title;
            Artist = artist;
            Easy = easy;
            Normal = normal;
        }
    }
}
