/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace FancyScrollView.Example01
{
    class ScrollView : FancyScrollView<ItemData>
    {
        [SerializeField] Scroller scroller = default;
        [SerializeField] GameObject cellPrefab = default;

        protected override GameObject CellPrefab => cellPrefab;

        protected override void Initialize()
        {
            base.Initialize();
            scroller.OnValueChanged(UpdatePosition);
        }

        public void UpdateData(IList<ItemData> items)
        {
            UpdateContents(items);
            scroller.SetTotalCount(items.Count); // データ数セット
        }

        private void Awake() // Startより前
        {
            // 表示用データを格納
            var datas = Enumerable.Range(1, 10)
                .Select(i => new ItemData($"Music {i}"))
                .ToList();

            this.UpdateData(datas);
        }
    }
}
