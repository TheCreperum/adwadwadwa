using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;
using adwadwadwa.Resources.DataHelper;
using adwadwadwa.Resources;
using adwadwadwa.Resources.Model;
using Android.Util;

namespace adwadwadwa
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.Design", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        ListView lstData;
        List<Resources.Model.Person> lstSource = new List<Resources.Model.Person>();
        DataBase db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            db = new DataBase();
            db.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            lstData = FindViewById<ListView>(Resource.Id.listView);

            var edtName = FindViewById<EditText>(Resource.Id.edtName);
            var edtAge = FindViewById<EditText>(Resource.Id.edtAge);
            var edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);

            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            LoadData();

            btnAdd.Click += delegate
            {
                Resources.Model.Person person = new Resources.Model.Person()
                {
                    Name = edtName.Text,
                    Age = int.Parse(edtAge.Text),
                    Email = edtEmail.Text
                };
                db.insertIntoTablePerson(person);
                LoadData();
            };
            btnEdit.Click += delegate {
                Resources.Model.Person person = new Resources.Model.Person()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    Age = int.Parse(edtAge.Text),
                    Email = edtEmail.Text
                };
                db.updateTablePerson(person);
                LoadData();
            };

            btnDelete.Click += delegate {
                Resources.Model.Person person = new Resources.Model.Person()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    Age = int.Parse(edtAge.Text),
                    Email = edtEmail.Text
                };
                db.deleteTablePerson(person);
                LoadData();
            };

            lstData.ItemClick += (s, e) => {
                for (int i = 0; i < lstData.Count; i++)
                {
                    if (e.Position == i)
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkGray);
                    else
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);

                }

                //Binding Data
                var txtName = e.View.FindViewById<TextView>(Resource.Id.textView1);
                var txtAge = e.View.FindViewById<TextView>(Resource.Id.textView2);
                var txtEmail = e.View.FindViewById<TextView>(Resource.Id.textView3);

                edtName.Text = txtName.Text;
                edtName.Tag = e.Id;

                edtAge.Text = txtAge.Text;

                edtEmail.Text = txtEmail.Text;

            };
        }

        private void LoadData()
        {
            lstSource = db.selectTablePerson();
            var adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}