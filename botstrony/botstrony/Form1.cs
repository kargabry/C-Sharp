using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace botstrony
{

    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {
          //  webBrowser1.Navigate("http://www.najpewniej.pl/dodaj.php?kat=764");
        }

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 100;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckForInternetConnection() == false)
                {
                    MessageBox.Show("Brak połączenia z internetem", "Błąd");
                }
                if (CheckForText() > 0)
                {
                    int count = CheckForText();
                    MessageBox.Show("Uzupełnij brakujące pola. Pozostało: " + count, "Puste pole");
                }
                if (CheckBox() == false)
                {
                    MessageBox.Show("Wybierz stronę", "Nie wybrano stron");
                }
            int start, stop, time;
            start = Environment.TickCount & Int32.MaxValue;

            if (CheckForInternetConnection() == true && CheckForText() == 0 && CheckBox() == true)
            {
                    if (mylomza_checkBox.Checked)
                    {
                        mylomza();
                    }
                    if (fourlomza_checkBox.Checked)
                    {
                        fourlomza();
                    }
                    if (kaliszak_checkBox.Checked)
                    {
                        kaliszak();
                    }
                    if (krakusik_checkBox.Checked)
                    {
                        krakusik();
                    }
                    if (gdyniak_checkBox.Checked)
                    {
                        gdyniak();
                    }
                    if (bazaro_checkBox.Checked)
                    {
                        bazaro();
                    }
                    if (najpewniej_checkBox.Checked)
                    {
                        najpewniej();
                    }
                    if (poznaniak_checkBox.Checked)
                    {
                        poznaniak();
                    }
                    if (wroclawiak_checkBox.Checked)
                    {
                        wroclawiak();
                    }
                    if (katowiczak_checkBox.Checked)
                    {
                        katowiczak();
                    }
                    if (bydgoszczak_checkBox.Checked)
                    {
                        bydgoszczak();
                    }
                    if (szczeciniak_checkBox.Checked)
                    {
                        szczeciniak();
                    }
                    if (gdaniak_checkBox.Checked)
                    {
                        gdaniak();
                    }
                    if (opolak_checkBox.Checked)
                    {
                        opolak();
                    }
                    if (toruniak_checkBox.Checked)
                    {
                        toruniak();
                    }
                    if (oswiecimiak_checkBox.Checked)
                    {
                        oswiecimiak();
                    }
                    if (lubliniak_checkBox.Checked)
                    {
                        lubliniak();
                    }


                    //jumla tez odpada bo obrazek
                    //epracazagranica.pl - trzeba sie zastanowic nad tym
                    stop = Environment.TickCount & Int32.MaxValue;
                    time = stop - start;
                    LoadingBar(progressBar1, time);
            }
            }
            catch
            {
                MessageBox.Show("Nieznany bład, skontaktuj się z Bartek&Karol Company ;)", "Fatality Error");
            }

        }

        private void mylomza()
        {
            try
            {
                webBrowser1.Document.GetElementById("contact").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("cat_id").SetAttribute("value", "6");

                HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("textarea");
                foreach (HtmlElement el in elc)
                {
                    if (el.GetAttribute("name").Equals("description"))
                    {
                        el.SetAttribute("value", tresc_textbox.Text);
                    }
                }

               /* HtmlElementCollection wcisk = this.webBrowser1.Document.GetElementsByTagName("button");
                foreach (HtmlElement el in wcisk)
                {
                    if (el.GetAttribute("type").Equals("button"))
                    {
                        el.InvokeMember("click");
                    }
                }
                * /

                //sprawdzenie czy dodalo ogloszenie
                /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                 {

                 }
                 */
            }
            catch
            {
                MessageBox.Show("Dodawanie do myLomza.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private void fourlomza()
        {
            try
            {
                webBrowser1.Navigate("https://www.4lomza.pl/ogl2.php?mod=new");
                webBrowser1.Document.GetElementById("form_email").InnerText = email_textbox.Text;
                webBrowser1.Document.GetElementById("form_nazwa").InnerText = name_textbox.Text;
                webBrowser1.Document.GetElementById("form_tel").InnerText = phone_textbox.Text;
                webBrowser1.Document.GetElementById("form_kat").SetAttribute("value", "4");
                webBrowser1.Document.GetElementById("form_tresc").InnerText = tresc_textbox.Text;

/*
                HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement el in elc)
                {
                    if (el.GetAttribute("type").Equals("submit"))
                    {
                        el.InvokeMember("click");
                    }
                }
 */

                //sprawdzenie czy dodalo ogloszenie
                /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                 {

                 }
                 */
            }
            catch
            {
                MessageBox.Show("Dodawanie do 4lomza.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private void bazaro()
        {
            try
            {
                webBrowser1.Navigate("https://bazaro.com.pl/?view=post&catid=5&subcatid=49");

                HtmlElementCollection ele = webBrowser1.Document.GetElementsByTagName("select");
                foreach (HtmlElement el in ele)
                {
                    if (el.GetAttribute("name").Equals("district"))
                    {
                        el.SetAttribute("value", "15");
                    }
                }

                webBrowser1.Document.GetElementById("email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("adtitle").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("addesc").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("price").SetAttribute("value", "123");
                webBrowser1.Document.GetElementById("x[1]").SetAttribute("value", phone_textbox.Text);

                HtmlElementCollection elem = webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement eles in elem)
                {
                    if (eles.GetAttribute("name").Equals("agree"))
                    {
                        eles.InvokeMember("click");
                    }
                }

 /*               HtmlElementCollection element = webBrowser1.Document.GetElementsByTagName("button");
                foreach (HtmlElement elems in element)
                {
                    if (elems.GetAttribute("class").Equals("btn btn-default btn-sm"))
                    {
                        elems.InvokeMember("click");
                    }
                }
  * /

                //sprawdzenie czy dodalo ogloszenie
                /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                 {

                 }
                 */
            }
            catch
            {
                MessageBox.Show("Dodawanie do bazarek.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private async void kaliszak()
        {
            try
            {
                webBrowser1.Navigate("http://www.kaliszak.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

              /*  HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement el in elc)
                {
                    if (el.GetAttribute("type").Equals("submit"))
                    {
                        el.InvokeMember("click");
                    }
                }
               * /

                //sprawdzenie czy dodalo ogloszenie
                /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                 {

                 }
                 */
            }
            catch
            {
                MessageBox.Show("Dodawanie do kaliszak.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private async void krakusik()
        {
            try
            {
                webBrowser1.Navigate("http://www.krakusik.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Praca za granicą")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");

    /*            HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement el in elc)
                {
                    if (el.GetAttribute("type").Equals("submit"))
                    {
                        el.InvokeMember("click");
                    }
                }
     */

                //sprawdzenie czy dodalo ogloszenie
              /*  if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                {

                }
              */
            }
            catch
            {
                MessageBox.Show("Dodawanie do krakusik.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private async void gdyniak()
        {
            try
            {
                webBrowser1.Navigate("http://www.gdyniak.pl/dodaj-ogloszenie/formularz");
                await PageLoad(5);

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

   /*             HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement el in elc)
                {
                    if (el.GetAttribute("type").Equals("submit"))
                    {
                        el.InvokeMember("click");
                    }
                }
    */

                //sprawdzenie czy dodalo ogloszenie
                /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                 {

                 }
                 */
            }
            catch
            {
                MessageBox.Show("Dodawanie do gdyniak.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private async void najpewniej()
        {
            try
            {
                webBrowser1.Navigate("http://www.najpewniej.pl/dodaj.php?kat=764");
                await PageLoad(5);

                HtmlElementCollection el = webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement ele in el)
                {
                    if (ele.GetAttribute("name").Equals("tytul"))
                    {
                        ele.SetAttribute("value", workstation_textBox.Text);
                    }

                    if (ele.GetAttribute("name").Equals("miasto"))
                    {
                        ele.SetAttribute("value", city_textbox.Text);
                    }

                    if (ele.GetAttribute("name").Equals("telk"))
                    {
                        ele.SetAttribute("value", phone_textbox.Text);
                    }

                    if (ele.GetAttribute("name").Equals("email"))
                    {
                        ele.SetAttribute("value", email_textbox.Text);
                    }
                    /*
                    if (ele.GetAttribute("name").Equals("dodaj"))
                    {
                        ele.InvokeMember("click");
                    } */
                }

                HtmlElementCollection elm = webBrowser1.Document.GetElementsByTagName("select");
                foreach (HtmlElement elem in elm)
                {
                    if (elem.GetAttribute("name").Equals("wojewodztwo"))
                    {
                        elem.SetAttribute("value", "18");
                    }
                }

                /*           HtmlElementCollection element = webBrowser1.Document.GetElementsByTagName("textarea");
                         foreach (HtmlElement elems in element)
                         {
                             if (elems.GetAttribute("name").Equals("tresc"))
                             {
                                 elems.SetAttribute("value", tresc_textbox.Text);
                             }
                         }
                 */

                         //sprawdzenie czy dodalo ogloszenie
                         /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                          {

                          }
                          */
            }
            catch
            {
                MessageBox.Show("Dodawanie do najpewniej.pl nie powiodło się.", "Niepowodzenie");
            }
        }
        private async void poznaniak()
        {
            try
            {
                webBrowser1.Navigate("http://www.poznaniak.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

                /*   HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                 foreach (HtmlElement el in elc)
                 {
                     if (el.GetAttribute("type").Equals("submit"))
                     {
                         el.InvokeMember("click");
                     }
                 }
                 */

                 //sprawdzenie czy dodalo ogloszenie
                 /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                 {

                 }
                 */
            }
            catch
            {
                MessageBox.Show("Dodawanie do poznaniak.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private async void wroclawiak()
        {
            try
            {
                webBrowser1.Navigate("http://www.wroclawiak.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

                /*        HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                      foreach (HtmlElement el in elc)
                      {
                          if (el.GetAttribute("type").Equals("submit"))
                          {
                              el.InvokeMember("click");
                          }
                      }
                 */

                      //sprawdzenie czy dodalo ogloszenie
                      /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                       {

                       }
                       */
            }
            catch
            {
                MessageBox.Show("Dodawanie do wroclawiak.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private async void katowiczak()
        {
            try
            {
                webBrowser1.Navigate("http://www.katowiczak.pl/dodaj-ogloszenie/formularz");
                await PageLoad(3);

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

                /*      HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                    foreach (HtmlElement el in elc)
                    {
                        if (el.GetAttribute("type").Equals("submit"))
                        {
                            el.InvokeMember("click");
                        }
                    }
                 */

                    //sprawdzenie czy dodalo ogloszenie
                    /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                   {

                   }
                   */
            }
            catch
            {
                MessageBox.Show("Dodawanie do katowiczak.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private async void bydgoszczak()
        {
            try
            {
                webBrowser1.Navigate("http://www.bydgoszczak.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

                /*       HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                     foreach (HtmlElement el in elc)
                     {
                         if (el.GetAttribute("type").Equals("submit"))
                         {
                             el.InvokeMember("click");
                         }
                     }
                 */

                     //sprawdzenie czy dodalo ogloszenie
                     /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                      {

                      }
                      */
            }
            catch
            {
                MessageBox.Show("Dodawanie do bydgoszczak.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private async void szczeciniak()
        {
            try
            {
                webBrowser1.Navigate("http://www.szczeciniak.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

                /*            HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                          foreach (HtmlElement el in elc)
                          {
                              if (el.GetAttribute("type").Equals("submit"))
                              {
                                  el.InvokeMember("click");
                              }
                          }
                 */

                          //sprawdzenie czy dodalo ogloszenie
                          /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                           {

                           }
                           */
            }
            catch
            {
                MessageBox.Show("Dodawanie do szczeciniak.pl nie powiodło się.", "Niepowodzenie");
            }
        }
        private async void gdaniak()
        {
            try
            {
                webBrowser1.Navigate("http://www.gdaniak.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

                /*         HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                       foreach (HtmlElement el in elc)
                       {
                           if (el.GetAttribute("type").Equals("submit"))
                           {
                               el.InvokeMember("click");
                           }
                       }
                 */

                       //sprawdzenie czy dodalo ogloszenie
                       /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                        {

                        }
                        */
            }
            catch
            {
                MessageBox.Show("Dodawanie do gdaniak.pl nie powiodło się.", "Niepowodzenie");
            }
        }
        private async void opolak()
        {
            try
            {
                webBrowser1.Navigate("http://www.opolak.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

                /*    HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                  foreach (HtmlElement el in elc)
                  {
                      if (el.GetAttribute("type").Equals("submit"))
                      {
                          el.InvokeMember("click");
                      }
                  }
                 */

                  //sprawdzenie czy dodalo ogloszenie
                  /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                   {

                   }
                   */
            }
            catch
            {
                MessageBox.Show("Dodawanie do opolak.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private async void toruniak()
        {
            try
            {
                webBrowser1.Navigate("http://www.toruniak.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

                /*      HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                    foreach (HtmlElement el in elc)
                    {
                        if (el.GetAttribute("type").Equals("submit"))
                        {
                            el.InvokeMember("click");
                        }
                    }
                 */

                    //sprawdzenie czy dodalo ogloszenie
                    /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                     {

                     }
                     */
            }
            catch
            {
                MessageBox.Show("Dodawanie do toruniak.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private async void oswiecimiak()
        {
            try
            {
                webBrowser1.Navigate("http://www.oswiecimiak.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

                /*           HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                         foreach (HtmlElement el in elc)
                         {
                             if (el.GetAttribute("type").Equals("submit"))
                             {
                                 el.InvokeMember("click");
                             }
                         }
                 */

                         //sprawdzenie czy dodalo ogloszenie
                         /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                          {

                          }
                          */
            }
            catch
            {
                MessageBox.Show("Dodawanie do oswiecimiak.pl nie powiodło się.", "Niepowodzenie");
            }
        }


        private async void lubliniak()
        {
            try
            {
                webBrowser1.Navigate("http://www.lubliniak.pl/dodaj-ogloszenie/formularz");

                HtmlElementCollection ele = webBrowser1.Document.All;
                foreach (HtmlElement el in ele)
                {
                    if (el.InnerText == "Dam pracę")
                    {
                        el.InvokeMember("onClick");
                    }

                }

                await PageLoad(5);

                webBrowser1.Document.GetElementById("announcement_title").SetAttribute("value", name_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_description").SetAttribute("value", tresc_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_phone_number").SetAttribute("value", phone_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_email").SetAttribute("value", email_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_place").SetAttribute("value", city_textbox.Text);
                webBrowser1.Document.GetElementById("announcement_attribute_109_Praca-za-granica").InvokeMember("click");
                webBrowser1.Document.GetElementById("announcement_rules_accept").InvokeMember("click");
                //webBrowser1.Document.GetElementById("announcement_marketing_consent").InvokeMember("click");

                /*      HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                    foreach (HtmlElement el in elc)
                    {
                        if (el.GetAttribute("type").Equals("submit"))
                        {
                            el.InvokeMember("click");
                        }
                    }
                 */

                    //sprawdzenie czy dodalo ogloszenie
                   /* if (webBrowser1.Url.ToString() == "tutaj link odpowiedni")
                    {

                    }
                    */
            }
            catch
            {
                MessageBox.Show("Dodawanie do lubliniak.pl nie powiodło się.", "Niepowodzenie");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int length = tresc_textbox.TextLength;

            if (length == 0)
            {
                time_label.Text = "";
            }
            else if (length < 160)
            {
                int remaind = 160 - length;
                time_label.Text = "Pozostało znaków: " + remaind;
            }
            else
            {
                time_label.Text = "Pozostało znaków: 0";
            }
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private int CheckForText(int count = 0) 
        {
            foreach (Control c in this.Controls)
            {

                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.Text == string.Empty)
                    {
                        count++;
                    }
                }
            }

            if (tresc_textbox.Text == string.Empty)
            {
                count++;
            }

            return count;
        }

        private async Task PageLoad(int TimeOut)
        {
            TaskCompletionSource<bool> PageLoaded = null;
            PageLoaded = new TaskCompletionSource<bool>();
            int TimeElapsed = 0;
            webBrowser1.DocumentCompleted += (s, e) =>
            {
                if (webBrowser1.ReadyState != WebBrowserReadyState.Complete) return;
                if (PageLoaded.Task.IsCompleted) return; PageLoaded.SetResult(true);
            };
            //
            while (PageLoaded.Task.Status != TaskStatus.RanToCompletion)
            {
                await Task.Delay(10);//10 ms interval
                TimeElapsed++;
                if (TimeElapsed >= TimeOut * 100) PageLoaded.TrySetResult(true);
            }
        }

        private void LoadingBar(System.Windows.Forms.ProgressBar progressBar, int time)
        {
            progressBar.Maximum = time;
            progressBar.Step = 1;

            for (int j = 0; j < time; j++)
            {
                double pow = Math.Pow(j, j); //Calculation
                progressBar.PerformStep();
            }

            if (progressBar1.Value == time)
            {
                progressBar1.Value = 1;
            }
        }
        public bool CheckBox()
        {
            bool check = false;
            foreach (var control in this.Controls)
            {
                if (control is CheckBox)
                {
                    if (((CheckBox)control).Checked)
                    {
                        check = true;
                    }
                }
            }

            if (check == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void zaznaczWszystkie_Click(object sender, EventArgs e)
        {
            foreach (var control in this.Controls)
            {
                if (control is CheckBox)
                {
                    if (((CheckBox)control).Checked)
                    {
                        ((CheckBox)control).Checked = false;
                    }
                    else
                    {
                        ((CheckBox)control).Checked = true;
                    }
                }
            }
        }

    }
}
