	
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Domain
{
    // ############################################################################
    // #
    // #        ---==>  T H I S  F I L E  W A S  G E N E R A T E D  <==---
    // #
    // # This file was generated by PRO Spark, C# Edition, Version 4.5
    // # Generated on 7/24/2013 4:11:01 PM
    // #
    // # Edits to this file may cause incorrect behavior and will be lost
    // # if the code is regenerated.
    // #
    // ############################################################################
	
	// Art Context. hosts all repositories

	public static class ArtContext
	{
	    static Db db = new ArtDb();
		
		// entity specific repositories

        public static Artists Artists { get { return new Artists(); } }
        public static Carts Carts { get { return new Carts(); } }
        public static CartItems CartItems { get { return new CartItems(); } }
        public static Errors Errors { get { return new Errors(); } }
        public static Orders Orders { get { return new Orders(); } }
        public static OrderDetails OrderDetails { get { return new OrderDetails(); } }
        public static OrderNumbers OrderNumbers { get { return new OrderNumbers(); } }
        public static Products Products { get { return new Products(); } }
        public static Ratings Ratings { get { return new Ratings(); } }
        public static Users Users { get { return new Users(); } }

        // general purpose operations

        public static void Execute(string sql, params object[] parms) { db.Execute( sql, parms ); }
        public static IEnumerable<dynamic> Query(string sql, params object[] parms) { return db.Query( sql, parms ); }
        public static object Scalar(string sql, params object[] parms) { return db.Scalar( sql, parms ); }

        public static DataSet GetDataSet(string sql, params object[] parms) { return db.GetDataSet( sql, parms ); }
        public static DataTable GetDataTable(string sql, params object[] parms) { return db.GetDataTable( sql, parms ); }
        public static DataRow GetDataRow(string sql, params object[] parms) { return db.GetDataRow( sql, parms ); }
	}
}