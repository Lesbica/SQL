using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SQL
{
    public partial class Main : Form
    {
        private readonly DbManager _dbManager;
        public Main()
        {
            InitializeComponent();
            _dbManager = new DbManager();
            _dbManager.ConnectTo();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Visitor visitor = new Visitor();
            visitor.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Result result = new Result();
            result.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Reagents_in_analysis reagentsInAnalysis = new Reagents_in_analysis();
            reagentsInAnalysis.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Reagent reagent = new Reagent();
            reagent.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Laborant_analysis laborantAnalysis = new Laborant_analysis();
            laborantAnalysis.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Lab_assistant labAssistant = new Lab_assistant();
            labAssistant.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Analysis_Type analysisType = new Analysis_Type();
            analysisType.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Analysis_Orders analysisOrders = new Analysis_Orders();
            analysisOrders.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Analysis analysis = new Analysis();
            analysis.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("visitor") && _dbManager.TableExists("orders") &&
                _dbManager.TableExists("analysistype") && _dbManager.TableExists("analysis") && 
                _dbManager.TableExists("labassistant") && _dbManager.TableExists("result") && 
                _dbManager.TableExists("analysisorders") && _dbManager.TableExists("laborantanalysis") && 
                _dbManager.TableExists("reagent") && _dbManager.TableExists("reagentsinanalysis"))
            {
                if (MessageBox.Show(@"you real want delete all table with all data?", @"Delete all tables",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _dbManager.DropTables();
                    button11.Enabled = false;
                    button12.Enabled = true;
                    for (int i = groupBox1.Controls.Count - 1; i >= 0; i--)
                    {
                        if (groupBox1.Controls[i] is Button)
                        {
                            groupBox1.Controls[i].Enabled = false;
                        }
                    }

                    for (int i = groupBox3.Controls.Count - 1; i >= 0; i--)
                    {
                        if (groupBox3.Controls[i] is Button)
                        {
                            groupBox3.Controls[i].Enabled = false;
                        }
                    }

                    for (int i = groupBox2.Controls.Count - 1; i >= 0; i--)
                    {
                        if (groupBox2.Controls[i] is Button)
                        {
                            groupBox2.Controls[i].Enabled = true;
                        }
                    }
                }
            }
            else
            {
                button11.Enabled = false;
            }
        }


        private void button12_Click(object sender, EventArgs e)
        {
            if (!_dbManager.TableExists("visitor") && !_dbManager.TableExists("orders") && !_dbManager.TableExists("analysistype") && 
                !_dbManager.TableExists("analysis") && !_dbManager.TableExists("labassistant") && !_dbManager.TableExists("result") &&
                !_dbManager.TableExists("analysisorders") && !_dbManager.TableExists("laborantanalysis") && !_dbManager.TableExists("reagent") &&
                !_dbManager.TableExists("reagentsinanalysis"))
            {
                List<string> tableQueries = new List<string>
            {
                "create table Visitor ( " +
                "ID int not null primary key, " +
                "FullName varchar(100) not null, " +
                "Age int check(Age > 0 and Age < 130) not null, " +
                "Allergies varchar(100) not null, " +
                "Contacts varchar(10) not null );",
                "create table Orders( " +
                "OrderNum int not null primary key, " +
                "foreign key (Visitors) references Visitor(ID), " +
                "Priority int not null check(Priority > 0 and Priority < 3) , " +
                "DueDate date not null, " +
                "TotalPrice double check(TotalPrice > 0.01), " +
                "Visitors int not null);",
                "create table AnalysisType( " +
                "CodeAnalType int not null primary key, " +
                "NameAnalType varchar(100) not null, " +
                "DescriptionAnalType varchar(100) not null);",
                "create table Analysis( " +
                "AnalCode int not null primary key, " +
                "foreign key (AnalysisType) references AnalysisType(CodeAnalType), " +
                "AnalysisType int not null, " +
                "Price double not null);",
                "create table LabAssistant( " +
                "ID int not null primary key, " +
                "FullName varchar(100) not null, " +
                "Specialization varchar(100) not null);",
                "create table Result(" +
                "CodeResult int not null primary key," +
                "ShortInfo varchar(100) not null," +
                "LongInfo text not null);",
                "create table AnalysisOrders( " +
                "foreign key (OrderNum) references Orders (OrderNum), " +
                "foreign key (AnalCode) references Analysis (AnalCode), " +
                "foreign key (Result) references Result (CodeResult), " +
                "DateOfResults date not null, " +
                "OrderNum      int  not null, " +
                "Result        int  not null, " +
                "AnalCode int not null, " +
                "LabAssistant int not null," +
                "foreign key (LabAssistant) references LabAssistant(ID));",
                "create table LaborantAnalysis(" +
                "AnalCode     int not null," +
                "LabAssistant int not null," +
                "foreign key (AnalCode) references Analysis (AnalCode)," +
                "foreign key (LabAssistant) references LabAssistant (ID));",
                "create table Reagent( " +
                "ReagCode int not null primary key, " +
                "NameReagent varchar(100) not null, " +
                "DescriptionReagent varchar(100) not null, " +
                "Available int not null);",
                "create table ReagentsInAnalysis(" +
                "UsedCount int not null," +
                "AnalCode int not null," +
                "ReagCode int not null," +
                "foreign key (AnalCode) references Analysis(AnalCode)," +
                "foreign key (ReagCode) references Reagent(ReagCode));"
            };
            _dbManager.CreateTables(tableQueries);
            button12.Enabled = false;
            button11.Enabled = true;
            for (int i = groupBox1.Controls.Count - 1; i >= 0; i--)
            {
                if (groupBox1.Controls[i] is Button)
                {
                    groupBox1.Controls[i].Enabled = true;
                }
            }
            
            for (int i = groupBox2.Controls.Count - 1; i >= 0; i--)
            {
                if (groupBox2.Controls[i] is Button)
                {
                    groupBox2.Controls[i].Enabled = false;
                }
            }

            for (int i = groupBox3.Controls.Count - 1; i >= 0; i--)
            {
                if (groupBox3.Controls[i] is Button)
                {
                    groupBox3.Controls[i].Enabled = true;
                }
            }
            }
            else
            {
                button12.Enabled = false;
            }
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("analysis"))
            {
                _dbManager.DropTable("analysis");
                button13.Enabled = false;
                button23.Enabled = true;
                button1.Enabled = false;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {

            if (!_dbManager.TableExists("analysis"))
            {
                if (!_dbManager.TableExists("analysistype"))
                {
                    button25_Click(sender, e);
                }
                _dbManager.CreateTable("Analysis",
                    "AnalCode int not null primary key, " +
                    "foreign key (AnalysisType) references AnalysisType(CodeAnalType), " +
                    "AnalysisType int not null, " +
                    "Price double not null "
                );
                button13.Enabled = true;
                button1.Enabled = true;
                button23.Enabled = false;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Dictionary<string, (Button buttonDelet, Button buttonShow, Button buttonAdd)> tableButtonMap = new Dictionary<string, (Button, Button, Button)>
            {
                {"analysis", (button13, button1, button23)},
                {"analysisorders", (button14, button2, button24)},
                {"analysistype", (button15, button3, button25)},
                {"labassistant", (button17, button4, button27)},
                {"laborantanalysis", (button19, button5, button29)},
                {"orders", (button21, button6, button31)},
                {"reagent", (button22, button7, button32)},
                {"reagentsinanalysis", (button20, button8, button30)},
                {"result", (button18, button9, button28)},
                {"visitor", (button16, button10, button26)}
            };
            int count = 0;
            
            foreach (var item in tableButtonMap)
            {
                string tableName = item.Key;
                (Button buttonDelet, Button buttonShow, Button buttonAdd) = item.Value;

                if (!_dbManager.TableExists(tableName))
                {
                    buttonDelet.Enabled = false;
                    buttonShow.Enabled = false;
                    buttonAdd.Enabled = true;
                    count++;
                }
            }

            if (count == 10)
            {
                button11.Enabled = false;
                button12.Enabled = true;
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("analysisorders"))
            {
                _dbManager.DropTable("analysisorders");
                button14.Enabled = false;
                button24.Enabled = true;
                button2.Enabled = false;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {

            if (!_dbManager.TableExists("analysisorders"))
            {
                if (!_dbManager.TableExists("orders"))
                {
                    button31_Click(sender, e);
                }

                if (!_dbManager.TableExists("result"))
                {
                    button28_Click(sender, e);
                }

                if (!_dbManager.TableExists("labassistant"))
                {
                    button27_Click(sender, e);
                }
                _dbManager.CreateTable("AnalysisOrders",
                    "foreign key (OrderNum) references Orders (OrderNum), " +
                    "foreign key (AnalCode) references Analysis (AnalCode), " +
                    "foreign key (Result) references Result (CodeResult), " +
                    "DateOfResults date not null, " +
                    "OrderNum      int  not null, " +
                    "Result        int  not null, " +
                    "AnalCode int not null, " +
                    "foreign key (LabAssistant) references LabAssistant(ID), " +
                    "LabAssistant int not null"
                );
                button14.Enabled = true;
                button2.Enabled = true;
                button24.Enabled = false;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("analysistype"))
            {
                _dbManager.DropTable("analysistype");
                button15.Enabled = false;
                button25.Enabled = true;
                button3.Enabled = false;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (!_dbManager.TableExists("analysistype"))
            {
                _dbManager.CreateTable("AnalysisType",
                    "CodeAnalType int not null primary key, " +
                    "NameAnalType varchar(100) not null, " +
                    "DescriptionAnalType varchar(100) not null"
                );
                button15.Enabled = true;
                button3.Enabled = true;
                button25.Enabled = false;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("labassistant"))
            {
                _dbManager.DropTable("labassistant");
                button17.Enabled = false;
                button27.Enabled = true;
                button4.Enabled = false;
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (!_dbManager.TableExists("labassistant"))
            {
                _dbManager.CreateTable("LabAssistant",
                    "ID int not null primary key, " +
                    "FullName varchar(100) not null, " +
                    "Specialization varchar(100) not null"
                );
                button17.Enabled = true;
                button4.Enabled = true;
                button27.Enabled = false;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("laborantanalysis"))
            {
                _dbManager.DropTable("laborantanalysis");
                button19.Enabled = false;
                button29.Enabled = true;
                button5.Enabled = false;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (!_dbManager.TableExists("laborantanalysis"))
            {
                if (!_dbManager.TableExists("labassistant"))
                {
                    button27_Click(sender, e);
                }
                _dbManager.CreateTable("LaborantAnalysis",
                    "AnalCode     int not null," +
                    "LabAssistant int not null," +
                    "foreign key (AnalCode) references Analysis (AnalCode)," +
                    "foreign key (LabAssistant) references LabAssistant (ID)"
                );
                button19.Enabled = true;
                button5.Enabled = true;
                button29.Enabled = false;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("orders"))
            {
                _dbManager.DropTable("orders");
                button21.Enabled = false;
                button31.Enabled = true;
                button6.Enabled = false;
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {

            if (!_dbManager.TableExists("orders"))
            {
                if (!_dbManager.TableExists("visitor"))
                {
                    button26_Click(sender, e);
                }
                _dbManager.CreateTable("Orders",
                    "OrderNum int not null primary key, " +
                    "foreign key (Visitors) references Visitor(ID), " +
                    "Priority int not null check(Priority > 0 and Priority < 3) , " +
                    "DueDate date not null, " +
                    "TotalPrice double check(TotalPrice > 0.01), " +
                    "Visitors int not null"
                );
                button21.Enabled = true;
                button6.Enabled = true;
                button31.Enabled = false;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("reagent"))
            {
                _dbManager.DropTable("reagent");
                button22.Enabled = false;
                button32.Enabled = true;
                button7.Enabled = false;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (!_dbManager.TableExists("reagent"))
            {
                _dbManager.CreateTable("Reagent",
                    "ReagCode int not null primary key, " +
                    "NameReagent varchar(100) not null, " +
                    "DescriptionReagent varchar(100) not null, " +
                    "Available int not null"
                );
                button22.Enabled = true;
                button7.Enabled = true;
                button32.Enabled = false;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("reagentsinanalysis"))
            {
                _dbManager.DropTable("reagentsinanalysis");
                button20.Enabled = false;
                button30.Enabled = true;
                button8.Enabled = false;
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (!_dbManager.TableExists("reagentsinanalysis"))
            {
                if (!_dbManager.TableExists("reagent"))
                {
                    button32_Click(sender, e);
                }
                
                _dbManager.CreateTable("ReagentsInAnalysis",
                    "UsedCount int not null," +
                    "AnalCode int not null," +
                    "ReagCode int not null, " +
                    "foreign key (AnalCode) references Analysis(AnalCode)," +
                     "foreign key (ReagCode) references Reagent(ReagCode)"
                );
                button20.Enabled = true;
                button8.Enabled = true;
                button30.Enabled = false;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("result"))
            {
                _dbManager.DropTable("result");
                button18.Enabled = false;
                button28.Enabled = true;
                button9.Enabled = false;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (!_dbManager.TableExists("result"))
            {
                _dbManager.CreateTable("Result",
                    "CodeResult int not null primary key," +
                    "ShortInfo varchar(100) not null," +
                    "LongInfo text not null"
                );
                button18.Enabled = true;
                button9.Enabled = true;
                button28.Enabled = false;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (_dbManager.TableExists("visitor"))
            {
                _dbManager.DropTable("visitor");
                button16.Enabled = false;
                button26.Enabled = true;
                button10.Enabled = false;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (!_dbManager.TableExists("visitor"))
            {
                _dbManager.CreateTable("Visitor",
                    "ID int not null primary key, " +
                    "FullName varchar(100) not null, " +
                    "Age int check(Age > 0 and Age < 130) not null, " +
                    "Allergies varchar(100) not null, " +
                    "Contacts varchar(10) not null"
                );
                button16.Enabled = true;
                button10.Enabled = true;
                button26.Enabled = false;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            sample sample = new sample();
            sample.ShowDialog();
        }
    }
}