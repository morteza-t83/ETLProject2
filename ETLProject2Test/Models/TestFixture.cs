using System.Data;
using ETLProject2.Models;

namespace ETLProject2Test.Models;

public class TestFixture
{
    public TestFixture()
    {
        var dataTable = new DataTable();
        dataTable.Clear();

        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("department", typeof(string));
        dataTable.Columns.Add("grade", typeof(int));
        dataTable.Columns.Add("University", typeof(string));

        dataTable.Rows.Add("Morteza", "CE", 17, "sharif");
        dataTable.Rows.Add("saeed", "CE", 17, "sharif");
        dataTable.Rows.Add("Mahdi", "CS", 18, "sharif");
        dataTable.Rows.Add("Ali", "CS", 19, "sharif");
        dataTable.Rows.Add("reza", "EE", 16, "sharif");
        dataTable.Rows.Add("sobhan", "CE", 20, "tehran");
        dataTable.Rows.Add("amirReza", "CE", 16, "tehran");
        dataTable.Rows.Add("amirAli", "CS", 18, "tehran");
        dataTable.Rows.Add("Mohammad", "CS", 15, "tehran");
        dataTable.Rows.Add("MohammadREza", "EE", 19, "tehran");
        DataTable = dataTable;
        DataBase.DataTables.Add("Students", dataTable);
        DataBase.DataTables.Add("DataTable1", new DataTable());
        DataBase.DataTables.Add("DataTable2", new DataTable());
        DataBase.DataTables.Add("DataTable3", new DataTable());
        DataBase.DataTables.Add("DataTable4", new DataTable());
    }

    public DataTable DataTable { get; private set; }
}