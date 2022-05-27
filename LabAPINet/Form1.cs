using System.Windows;

namespace LabAPINet
{
    public partial class Form1 : Form
    {
        HttpClient client;
        int id = 0;

        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7284/api/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HttpResponseMessage respond = client.GetAsync("Courses").Result;
            if (respond.IsSuccessStatusCode)
            {
                List<Course> course = respond.Content.ReadAsAsync<List<Course>>().Result;
                dgv_display.DataSource = course;
            }
            else
            {
                MessageBox.Show("error");
            }

            HttpResponseMessage mssg = client.GetAsync("Tracks").Result;
            if (mssg.IsSuccessStatusCode)
            {
                List<Track> track = mssg.Content.ReadAsAsync<List<Track>>().Result;
                cb_track.DataSource = track;
                cb_track.DisplayMember = "Name";
                cb_track.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show("error");
            }

            btn_update.Visible = false;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Course crs = new Course()
            {
                Name = txt_name.Text,
                Describtion = txt_description.Text,
                Duration = Double.Parse(txt_duration.Text),
                TrackId = (int)cb_track.SelectedValue
            };

            HttpResponseMessage respond = client.PostAsJsonAsync("Courses", crs).Result;
            if (respond.IsSuccessStatusCode)
            {
                Form1_Load(null, null);
                txt_duration.Text = txt_description.Text = txt_name.Text = "";
            }
            else
            {
                MessageBox.Show(respond.StatusCode.ToString());
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            Course crs = new Course()
            {
                Id = id,
                Name = txt_name.Text,
                Describtion = txt_description.Text,
                Duration = Double.Parse(txt_duration.Text),
                TrackId = (int)cb_track.SelectedValue
            };

            HttpResponseMessage respond = client.PutAsJsonAsync($"Courses/{id}", crs).Result;
            if (respond.IsSuccessStatusCode)
            {
                Form1_Load(null, null);
                txt_duration.Text = txt_description.Text = txt_name.Text = "";
                btn_add.Visible = true;
            }
            else
            {
                MessageBox.Show(respond.StatusCode.ToString());
            }
        }

        private void dgv_display_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = (int)dgv_display.SelectedRows[0].Cells[0].Value;
            txt_name.Text = dgv_display.SelectedRows[0].Cells[1].Value.ToString();
            txt_description.Text = dgv_display.SelectedRows[0].Cells[2].Value.ToString();
            txt_duration.Text = dgv_display.SelectedRows[0].Cells[3].Value.ToString();
            cb_track.SelectedValue = dgv_display.SelectedRows[0].Cells[4].Value;

            btn_update.Visible = true;
            btn_add.Visible = false;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to delete this Course?", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var response = client.DeleteAsync($"Courses/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    Form1_Load(null, null);
                    txt_duration.Text = txt_description.Text = txt_name.Text = "";
                }
                else
                {
                    MessageBox.Show(response.StatusCode.ToString());
                }
            }            
        }
    }
}