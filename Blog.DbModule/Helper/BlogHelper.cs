using Common.FreeSql;

namespace Blog.DbModule.Helper;

public static class BlogHelper
{
    public static IFreeSql GetDatabase(this FreeSqlResolver resolver)
    {
        return resolver(BlogDbModule.BlogDatabaseName);
    }
}