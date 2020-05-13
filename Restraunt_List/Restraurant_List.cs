using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Restaurant_List
{
    public partial class Restraurant_List : Form
    {
        List<Restaurant> restaurants;

        public Restraurant_List()
        {
            InitializeComponent();
            restaurants = new List<Restaurant>();


        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;
            string menu = tbMenu.Text;
            string location = tbLocation.Text;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("가게명이 공백이 입력되었습니다. 다시 입력하세요.");
                return;
            }
            if (string.IsNullOrEmpty(menu))
            {
                MessageBox.Show("메뉴가 공백이 입력되었습니다. 다시 입력하세요.");
                return;
            }
            if (string.IsNullOrEmpty(location))
            {
                MessageBox.Show("위치가 공백이 입력되었습니다. 다시 입력하세요.");
                return;
            }
            try
            {
                for (int i = 0; i < dgvRestaurant.Rows.Count - 1; i++)
                {
         
                    if (dgvRestaurant.Rows[i].Cells[0].Value.ToString() == name)
                    {
                        MessageBox.Show("이미 입력된 가게명이 입력되었습니다. 다시 입력하세요.");
                        return;
                    }
                    Thread.Sleep(5);
                }
                Restaurant tempRes = new Restaurant() { name = name, menu = menu, location = location };

                restaurants.Add(tempRes);

                dgvRestaurant.Rows.Add(tempRes.name, tempRes.menu, tempRes.location);
                clearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int totalRowCount = dgvRestaurant.Rows.Count;
            int currentRowIndex = dgvRestaurant.CurrentRow.Cells[0].RowIndex;

            //빈칸을 삭제할 시 예외처리

            if (totalRowCount == (currentRowIndex + 1))
            {
                return;
            }
            //MessageBox.Show(Convert.ToString(dgvRestaurant.Rows.Count.ToString()));
            //MessageBox.Show(Convert.ToString(dgvRestaurant.CurrentRow.Cells[0].RowIndex));
            //MessageBox.Show(Convert.ToString(dgvRestaurant.CurrentRow.Cells[0].Value));
            dgvRestaurant.Rows.Remove(dgvRestaurant.Rows[currentRowIndex]);
            clearInput();
        }
        private void clearInput()
        {
            tbName.Text = "";
            tbMenu.Text = "";
            tbLocation.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (TextWriter writer = new StreamWriter("../../../data.txt", true))
                {
                    for (int i = 0; i < dgvRestaurant.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgvRestaurant.Columns.Count; j++)
                        {
                            writer.Write(dgvRestaurant.Rows[i].Cells[j].Value.ToString() + "|");
                            Thread.Sleep(10);
                        }
                        writer.WriteLine("");
                    }
                    writer.Close();
                    MessageBox.Show("파일저장이 완료되었습니다.");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            try
            {
                int currentRowIndex = dgvRestaurant.CurrentRow.Cells[0].RowIndex;
                string name = tbName.Text;
                string menu = tbMenu.Text;
                string location = tbLocation.Text;

                //공백체크
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("가게명이 공백이 입력되었습니다. 다시 입력하세요.");
                    return;
                }
                if (string.IsNullOrEmpty(menu))
                {
                    MessageBox.Show("메뉴가 공백이 입력되었습니다. 다시 입력하세요.");
                    return;
                }
                if (string.IsNullOrEmpty(location))
                {
                    MessageBox.Show("위치가 공백이 입력되었습니다. 다시 입력하세요.");
                    return;
                }
                //가게명이 자주 바뀌지 않으므로 뺴둠
                if (dgvRestaurant.CurrentRow.Cells[0].Value.ToString() == tbName.Text.ToString())
                {
                    dgvRestaurant.CurrentRow.Cells[1].Value = tbMenu.Text.ToString();
                    dgvRestaurant.CurrentRow.Cells[2].Value = tbLocation.Text.ToString();
                    Thread.Sleep(10);
                    clearInput();
                }
                else
                {
                    MessageBox.Show("가게이름이 새로 바뀌었나보네요.");
                    dgvRestaurant.CurrentRow.Cells[0].Value = tbName.Text.ToString();
                    dgvRestaurant.CurrentRow.Cells[1].Value = tbMenu.Text.ToString();
                    dgvRestaurant.CurrentRow.Cells[2].Value = tbLocation.Text.ToString();

                    clearInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void dgvRestaurant_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int totalRowCount = dgvRestaurant.Rows.Count;
                int currentRowIndex = dgvRestaurant.CurrentRow.Cells[0].RowIndex;

                //빈칸을 클릭할 시 예외처리

                if (totalRowCount == (currentRowIndex + 1))
                {
                    return;
                }
                else
                {
                    tbName.Text = dgvRestaurant.CurrentRow.Cells[0].Value.ToString();
                    tbMenu.Text = dgvRestaurant.CurrentRow.Cells[1].Value.ToString();
                    tbLocation.Text = dgvRestaurant.CurrentRow.Cells[2].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



            //string name = tbName.Text;
            //string menu = tbMenu.Text;
            //string location = tbLocation.Text;

        }
        private void btnCall_Click(object sender, EventArgs e)
        {
            OpenFileDialog();

        }

        private void OpenFileDialog()
        {
            using (OpenFileDialog opd = new OpenFileDialog())
            {
                opd.DefaultExt = "All files";
                opd.Filter = "(*.txt)|*.txt";//파일타입
                opd.Multiselect = false;
                string strAppDir = "../../"+System.Windows.Forms.Application.StartupPath;

                opd.InitialDirectory = strAppDir;

                if (opd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = opd.FileName;

                    if (MessageBox.Show("파일을 불러오시겠습니까?\n" +
                        "저장하세요! 지금까지 작업한 내용 모두가 손실될 수 있습니다.", "불러오기", MessageBoxButtons.YesNo) != DialogResult.Abort)
                    {
                        //모두 삭제 
                        dgvRestaurant.Rows.Clear();
                    }
                    ////Open
                    string[] allData = System.IO.File.ReadAllLines(fileName);
                    if(allData.Length >0)
                    {
                    
                        for (int i=0; i< allData.Length;i++)
                        {
                            string [] oneLineData= allData[i].Split('|');

                            dgvRestaurant.Rows.Add(oneLineData[0],oneLineData[1],oneLineData[2]);
                            Thread.Sleep(10);
                            //string OneLineDatat = allData[i].Split(' ');
                            //dgvRestaurant.Rows.Add(tempRes.name, tempRes.menu, tempRes.location);
                        }

                        MessageBox.Show("파일 불러오기 성공");
                    }
                    else
                    {
                        MessageBox.Show("파일이 비어있습니다.");
                    }

                }

            }
        }

        private void dgvRestaurant_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //private Restaurant ReadTextFileToList(string fileName)
        //{

        //}

    }
}
