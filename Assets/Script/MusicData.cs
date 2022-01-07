/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System;
using UnityEngine;

namespace FancyScrollView.Example01
{
    class MusicData // 個々の楽曲データ
    {
        public string Title { get; }
        public string Artist { get; }
        public int Easy { get; }
        public int Normal { get; }
        public string Chart { get; }

        public MusicData(string title, string artist, int easy, int normal)
        {
            Title = title;
            Artist = artist;
            Easy = easy;
            Normal = normal;
        }
    }
}
