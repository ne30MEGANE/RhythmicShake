/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.Example01
{
    class Cell : FancyCell<ItemData>
    {
        [SerializeField] Text message = default;

        public override void UpdateContent(ItemData itemData)
        {
            message.text = itemData.Title;
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
