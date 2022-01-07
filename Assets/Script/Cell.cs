/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.Example01
{
    class Cell : FancyCell<MusicData>
    {
        [SerializeField] Text title = default;
        [SerializeField] Text artist = default;
        [SerializeField] Text easy = default;
        [SerializeField] Text normal = default;

        public override void UpdateContent(MusicData music)
        {
            title.text = music.Title;
            artist.text = music.Artist;
            easy.text = music.Easy.ToString();
            normal.text = music.Normal.ToString();
        }

        public override void UpdatePosition(float position)
        {
            // スクロールの挙動
            var pos = transform.localPosition;
            pos.y = Mathf.Lerp( 100, -1100, position );
            transform.localPosition = pos;
        
        }

    }
}
