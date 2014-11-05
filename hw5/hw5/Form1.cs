using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//
// GUI-based Asian Options Stock Pricing program.
//
// Antonio Villarreal
// U. of Illinois, Chicago
// CS341, Fall 2014
// Homework 5
//

namespace hw5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); //Initializes all components
        }

        //Gets user input from each textbox if there is any
        private void getInputs(ref double initPrice, ref double exerPrice, ref double upperBound, ref double lowerBound, ref double interRate, ref long time, ref long simRuns)
        {   
            //Checks if there is null or white space in text. if not stores whatever the user input otherwise it uses the default value
            if (!string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                initPrice = System.Convert.ToDouble(this.textBox1.Text);
            }
            else
            {
                initPrice = 30.00;
            }
           
            //Checks if there is null or white space in text. if not stores whatever the user input otherwise it uses the default value
            if (!string.IsNullOrWhiteSpace(this.textBox2.Text))
            {
                exerPrice = System.Convert.ToDouble(this.textBox2.Text);
            }
            else
            {
                exerPrice = 30.00;
            }

            //Checks if there is null or white space in text. if not stores whatever the user input otherwise it uses the default value
            if (!string.IsNullOrWhiteSpace(this.textBox3.Text))
            {
                upperBound = System.Convert.ToDouble(this.textBox3.Text);
            }
            else
            {
                upperBound = 1.40;
            }

            //Checks if there is null or white space in text. if not stores whatever the user input otherwise it uses the default value
            if (!string.IsNullOrWhiteSpace(this.textBox4.Text))
            {
                lowerBound = System.Convert.ToDouble(this.textBox4.Text);
            }
            else
            {
                lowerBound = 0.80;
            }

            //Checks if there is null or white space in text. if not stores whatever the user input otherwise it uses the default value
            if (!string.IsNullOrWhiteSpace(this.textBox5.Text))
            {
                interRate = System.Convert.ToDouble(this.textBox5.Text);
            }
            else
            {
                interRate = 1.08;
            }

            //Checks if there is null or white space in text. if not stores whatever the user input otherwise it uses the default value
            if (!string.IsNullOrWhiteSpace(this.textBox6.Text))
            {
                time = System.Convert.ToInt64(this.textBox6.Text);
            }
            else
            { 
                time = 30;
            }

            //Checks if there is null or white space in text. if not stores whatever the user input otherwise it uses the default value
            if (!string.IsNullOrWhiteSpace(this.textBox7.Text))
            {
                simRuns = System.Convert.ToInt64(this.textBox7.Text);
            }
            else
            {
                simRuns = 5000000;
            }

            this.richTextBox1.AppendText("******Initial Value: " + initPrice + "  *******" + Environment.NewLine);
            this.richTextBox1.AppendText("******Exercise Value: " + exerPrice + "  *******" + Environment.NewLine);
            this.richTextBox1.AppendText("******Upper Bound Value: " + upperBound + "  *******" + Environment.NewLine);
            this.richTextBox1.AppendText("******Lower Bound Value: " + lowerBound + "  *******" + Environment.NewLine);
            this.richTextBox1.AppendText("******Interest Value: " + interRate + "  *******" + Environment.NewLine);
            this.richTextBox1.AppendText("******Time Value: " + time + "  *******\n");
            this.richTextBox1.AppendText("******Simulation Value: " + simRuns + "  *******" + Environment.NewLine + Environment.NewLine);
        }

        //Method to process the button click
        private async void button1_Click(object sender, EventArgs e)
        {
            double initPrice = 0.0;     //Initialize variables
            double exerPrice = 0.0;
            double upperBound = 0.0;
            double lowerBound = 0.0;
            double interRate = 0.0;
            long time = 0;
            long simRuns = 0;

            //Calls getInputs to get input from the text boxes
            getInputs(ref initPrice, ref exerPrice, ref upperBound, ref lowerBound, ref interRate, ref time, ref simRuns);
            
            //Appends the following to the output text box
            this.richTextBox1.AppendText("******Simulation Started*******\n");

            //Starts the timer
            int start = System.Environment.TickCount;

            //Runs the task asynchonously
            double price = await Task.Run( () => AsianOptionsPricing.Simulation(initPrice, exerPrice, upperBound, lowerBound, interRate, time, simRuns));

            //Stops the timer
            int stop = System.Environment.TickCount;
            double elapsedTimeInSecs = (stop - start) / 1000.0; //Calculate time passed
            
            //Appends the following to output text box
            this.richTextBox1.AppendText("******Simulation Stopped*******" + Environment.NewLine + Environment.NewLine);
            this.richTextBox1.AppendText("Price: "+price.ToString()+Environment.NewLine);
            this.richTextBox1.AppendText("Time: " + elapsedTimeInSecs.ToString() + Environment.NewLine + Environment.NewLine);

        }

        
    }
}
