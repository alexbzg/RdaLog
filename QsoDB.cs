using System;
using LiteDB;

namespace tnxlog { }

public class QsoDB
{
    string _dbFilePath;
    var _db;
    var _qsoCollection;

    public QsoDB(string filePath)
    {
        _dbFilePath = filePath;
        _db = new LiteDatabase(_dbFilePath);
        _qsoCollection = db.GetCollection<QSO>("qso");
    }

    public insert(QSO qso)
    {

    }
}
