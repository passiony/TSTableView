using UnityEngine;
using System.Collections;
using Tacticsoft;
using System.Collections.Generic;

namespace Tacticsoft.Examples
{
    //An example implementation of a class that communicates with a TableView
    public class SimpleTableViewController : MonoBehaviour, ITableViewDataSource
    {
        public VisibleCounterCell m_cellPrefab;
        public TableView m_tableView;

        private int m_numInstancesCreated = 0;

        //data source，you need make it self
        List<string> m_data = new List<string>() { "湖南", "湖北", "山东", "山西", "河南", "河北", "广东", "广西", "黑龙江", "内蒙古", "新疆", "西藏", "台湾", "香港", "澳门" };

        //Register as the TableView's delegate (required) and data source (optional)
        //to receive the calls
        void Start() {
            //Init TableView
            m_tableView.dataSource = this;
            m_tableView.onCellVisibilityChanged = TableViewCellVisibilityChanged;
        }

        public void SendBeer()
        {
            Application.OpenURL("https://www.paypal.com/cgi-bin/webscr?business=contact@tacticsoft.net&cmd=_xclick&item_name=Beer%20for%20TSTableView&currency_code=USD&amount=5.00");
        }

        #region ITableViewDataSource

        //Will be called by the TableView to know how many rows are in this table
        public int GetNumberOfRowsForTableView(TableView tableView) {
            return m_data.Count;
        }

        //Will be called by the TableView to know what is the height of each row
        public float GetHeightForRowInTableView(TableView tableView, int row) {
            return (m_cellPrefab.transform as RectTransform).rect.height;
        }

        //Will be called by the TableView when a cell needs to be created for display
        public TableViewCell GetCellForRowInTableView(TableView tableView, int row) {
            VisibleCounterCell cell = tableView.GetReusableCell(m_cellPrefab.reuseIdentifier) as VisibleCounterCell;
            if (cell == null) {
                cell = (VisibleCounterCell)GameObject.Instantiate(m_cellPrefab);
                cell.name = "VisibleCounterCellInstance_" + (++m_numInstancesCreated).ToString();
                //bind cell's click event
                cell.onClick = OnCellClick;
            }

            //refresh cell info
            cell.SetRowNumber(row);
            cell.SetText(m_data[row]);
            return cell;
        }

        #endregion

        #region Table View event handlers

        //Will be called by the TableView when a cell's visibility changed
        public void TableViewCellVisibilityChanged(int row, bool isVisible) {
            //Debug.Log(string.Format("Row {0} visibility changed to {1}", row, isVisible));
            if (isVisible) {
                VisibleCounterCell cell = (VisibleCounterCell)m_tableView.GetCellAtRow(row);
                cell.NotifyBecameVisible();
            }
        }

        //Will be called when the click the cell 
        public void OnCellClick(int row)
        {
            m_data.RemoveAt(row);
            m_tableView.ReloadData();
        }

        #endregion

    }
}
