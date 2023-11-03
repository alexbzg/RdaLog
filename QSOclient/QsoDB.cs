﻿using System;
using LiteDB;

namespace tnxlog
{

    public class QsoDB
    {
        private string _dbFilePath;
        private LiteDatabase _db;
        public ILiteCollection<QSO> qso;

        public QsoDB(string filePath)
        {
            _dbFilePath = filePath;
            _db = new LiteDatabase(_dbFilePath);
            qso = _db.GetCollection<QSO>("qso");
        }
    }
}