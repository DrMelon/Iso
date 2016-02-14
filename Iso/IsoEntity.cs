using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Iso
{
    class IsoEntity : Entity
    {

        public Vector3 IsometricPosition;
        public Vector3 IsometricVelocity;
        public Vector3 IsometricAcceleration;

        public int IsometricWidth = 32;
        public int IsometricHeight = 16;

        public float OffsetX, OffsetY;

        // This is for making isometrically-bound objects.
        

        public override void Update()
        {
            base.Update();

            IsometricVelocity += IsometricAcceleration;
            if(IsometricAcceleration.Length() < 0.01f)
            {
                IsometricVelocity *= 0.6f;
            }

            IsometricPosition += IsometricVelocity;


            X = OffsetX + (IsometricPosition.X * (IsometricWidth / 2) + IsometricPosition.Y * (IsometricWidth / 2));
            Y = OffsetY - (IsometricPosition.Z * (IsometricHeight / 2) + IsometricPosition.Y * (IsometricHeight / 2) - IsometricPosition.X * (IsometricHeight / 2));
            Layer = (int)(-(2*IsometricPosition.Z) + IsometricPosition.Y - (IsometricPosition.X * 0.5f));


        }

        public void MoveToIsoPosition(Vector3 NewPosition)
        {
            IsometricPosition = NewPosition;

            X = OffsetX + (IsometricPosition.X * (IsometricWidth / 2) + IsometricPosition.Y * (IsometricWidth / 2));
            Y = OffsetY - (IsometricPosition.Z * (IsometricHeight / 2) + IsometricPosition.Y * (IsometricHeight / 2) - IsometricPosition.X * (IsometricHeight / 2));
            Layer = (int)(-(2*IsometricPosition.Z) + IsometricPosition.Y - (IsometricPosition.X * 0.5f));
        }





    }
}
