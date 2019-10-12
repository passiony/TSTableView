using UnityEngine;
using System.Collections;
using Tacticsoft;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Tacticsoft.Examples
{
    //Inherit from TableViewCell instead of MonoBehavior to use the GameObject
    //containing this component as a cell in a TableView
    public class VisibleCounterCell : TableViewCell
    {
        public Text m_rowNumberText;
        public Text m_visibleCountText;
        public Image m_background;

        /// <summary>
        /// the delegate for cell bind the click event
        /// the param(int) is the cell's row num
        /// </summary>
        public Action<int> onClick;

        void Start()
        {
            var button = this.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                //Debug.Log("button.onClick" + m_rowNumber);
                if (onClick != null)
                    onClick(m_rowNumber);
            });
        }

        private int m_rowNumber;
        public void SetRowNumber(int rowNumber) {
            m_rowNumber = rowNumber;
            m_background.color = GetColorForRow(rowNumber);
        }

        private string m_info;
        public void SetText(string info)
        {
            m_info = info;
            m_rowNumberText.text = "Row " + m_rowNumber.ToString() + ";Info:" + info;
        }

        private int m_numTimesBecameVisible;
        public void NotifyBecameVisible() {
            m_numTimesBecameVisible++;
            m_visibleCountText.text = "# rows this cell showed : " + m_numTimesBecameVisible.ToString();
        }

        private Color GetColorForRow(int row) {
            switch (row % 3) {
                case 0:
                    return Color.gray;
                case 1:
                    return Color.white;
                default:
                    return Color.red;
            }
        }
    }
}
