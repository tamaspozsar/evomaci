/* -------------------------------------------------------------------------------------------------
   Restricted - Copyright (C) Siemens Healthcare GmbH/Siemens Medical Solutions USA, Inc., 2018. All rights reserved
   ------------------------------------------------------------------------------------------------- */

namespace MaciLaci.Backend
{
    public class Empty : FieldObject
    {
        public Empty(Coordinate coordinate): base(coordinate.Row, coordinate.Column)
        {
            
        }
        public Empty(int x, int y) : base(x, y)
        {
        }
    }
}
