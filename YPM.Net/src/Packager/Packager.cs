using stLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPM.Packager {
    public class ypmPackage {
        public readonly string AtMark;

        public ypmPackage( string operation, string[] ps )
        {
            checkOperation( operation );

            AtMark = "@";
            List<string> paraments = new List<string>();
            foreach ( var p in ps ) {
                if ( string.IsNullOrEmpty( p ) ) {
                    continue;
                }

                // 检查最多的@字符串并设置目前最大的该串
                while ( true ) {
                    var wrappedAt = StringHelper.WrapWith( AtMark, "[", "]" );
                    if ( StringHelper.SubstringCount( p, wrappedAt ) == 0 ) {
                        break;
                    } else {
                        AtMark = wrappedAt;
                    }
                }
                // 将',' '\'转义
                paraments.Add(reverse( p ));
            }
            Paraments = paraments.ToArray();
        }

        private string checkOperation( string operation )
        {
            if ( operation.Contains( "@" ) || operation.Contains( "," ) ) {
                throw new Exception( "Invalid operation name called: " + operation );
            }
            Operation = operation;
            return operation;
        }

        public string Operation { get; set; }
        public string[] Paraments { get; set; }

        public string reverse( string p )
        {
            for ( Int32 i = 0; i < p.Length; i++ ) {
                Char c = p[i];
                if ( c == ',' || c == '\\' ) {
                    p = p.Insert( i, "\\" );
                    i++;
                }
            }
            return p;
        }

        public override string ToString()
        {
            string msg = "";
            msg += Operation + AtMark;
            for ( Int32 i = 0; i < Paraments.Length; i++ ) {
                String p = Paraments[i];
                msg += Paraments[i] + ( i == Paraments.Length - 1 ? "" : "," );
            }
            return msg;
        }
    }
}
