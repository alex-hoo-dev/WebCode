namespace WebCodeCli.Domain.Common.Options
{
    public class DBConnectionOption
    {
        /// <summary>
        /// 数据库类型（Sqlite, PostgreSQL, MySql, SqlServer）
        /// </summary>
        public string DbType { get; set; } = "Sqlite";
        
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionStrings { get; set; } = "Data Source=WebCodeCli.db";
        
        /// <summary>
        /// 全局实例，用于静态访问
        /// </summary>
        public static DBConnectionOption Instance { get; set; } = new DBConnectionOption();
    }
}
