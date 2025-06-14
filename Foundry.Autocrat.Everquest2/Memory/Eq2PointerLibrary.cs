using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundry.Autocrat.Memory;
using Foundry.Autocrat.Extensions.Memory;
using System.Xml.Linq;
using System.Diagnostics;
using Foundry.Autocrat.Geometry;

using Foundry.Autocrat.Extensions.Math;
using System.IO;

namespace Foundry.Autocrat.Everquest2.Memory
{
    public class Eq2
    {
        internal Eq2PointerLibrary eq2Pointers;

        public Eq2(Process eq2Process, string pointerDocumentPath)
        {
            eq2Pointers = new Eq2PointerLibrary(eq2Process, pointerDocumentPath);

            Character = new CharacterAccessor { Parent = this };
            Target = new TargetAccessor { Parent = this };
            UI = new UIAccessor { Parent = this };
            Camera = new CameraAccessor { Parent = this };
            Autocrat = new AutocratAccessor { Parent = this };
        }

        public Process Eq2Process { get { return eq2Pointers.Eq2Process; } }

        public CharacterAccessor Character { get; set; }
        public TargetAccessor Target { get; set; }
        public UIAccessor UI { get; set; }
        public CameraAccessor Camera { get; set; }
        public AutocratAccessor Autocrat { get; set; }





        internal class Eq2PointerLibrary : PointerLibrary
        {
            private static class A
            {
                public static byte[] B(byte[] s, byte[] a)
                {
                    byte[] r = (byte[])s.Clone();
                    for (int i = 0; i < r.Length; i++) r[i] = (byte)((r[i] | a[i % (a.Length)]) & ~(r[i] & a[i % (a.Length)]));
                    return r;
                }
                public static byte[] B(string path, byte[] a)
                {
                    return B(File.ReadAllBytes(path), a);
                }
            }



            public Process Eq2Process { get; set; }

            public Eq2PointerLibrary(Process eq2Process, string pointerDocumentPath)
            {
                this.Eq2Process = eq2Process;

                byte[] k = new byte[] {
                0xd4, 0x94, 0xee, 0x9b, 0x3d, 0xe2, 0x2e, 0xd2,
                0xf2, 0xd4, 0xb5, 0x64, 0x02, 0xbd, 0xaf, 0x93,
                0x37, 0x8a, 0xad, 0xd7, 0xb3, 0x08, 0x07, 0xb4,
                0xf1, 0x0b, 0x18, 0x42, 0xa5, 0x68, 0x8f, 0x10,
                0xea, 0xb8, 0x8b, 0x56, 0x0f, 0xa9, 0x8f, 0x52,
                0x53, 0x06, 0xd2, 0x84, 0xe8, 0x64, 0x56, 0xca,
                0x82, 0x55, 0x71, 0x79, 0x48, 0xcb, 0x9b, 0xcc,
                0x8d, 0x3a, 0xaa, 0x32, 0xc9, 0xec, 0x2d, 0x89,
                0x45, 0xa4, 0x7a, 0x49, 0x32, 0x58, 0x83, 0x8e,
                0xfb, 0x5a, 0x83, 0xad, 0x36, 0x83, 0xcc, 0x8d,
                0xb4, 0x4e, 0xbf, 0x50, 0xf7, 0x16, 0xc8, 0x1e,
                0x1d, 0xbf, 0xc8, 0xd5, 0xff, 0x5d, 0x89, 0x67,
                0xd9, 0x48, 0x3f, 0x43, 0xea, 0x0c, 0x98, 0x2a,
                0xd1, 0xe3, 0x7d, 0xee, 0x48, 0xee, 0x95, 0x0a,
                0x6e, 0x1c, 0x7d, 0x7b, 0x2d, 0xdd, 0x92, 0x2e,
                0xdc, 0x5b, 0x2c, 0x73, 0x8f, 0xad, 0x8e, 0x9e
            };

                string s = Encoding.ASCII.GetString(A.B(pointerDocumentPath, k)).Substring(3);
                this.PointerDocument = XDocument.Parse(s);
            }

            #region Character

            public Vector3 CharacterLocation
            {
                get { return Read<Vector3>(Eq2Process, "Character Movement/Location/X Axis"); }
                set { Write(Eq2Process, "Character Movement/Location/X Axis", value); }
            }

            public float CharacterX
            {
                get { return Read<float>(Eq2Process, "Character Movement/Location/X Axis"); }
                set { Write(Eq2Process, "Character Movement/Location/X Axis", value); }
            }

            public float CharacterY
            {
                get { return Read<float>(Eq2Process, "Character Movement/Location/Y Axis"); }
                set { Write(Eq2Process, "Character Movement/Location/Y Axis", value); }
            }

            public float CharacterZ
            {
                get { return Read<float>(Eq2Process, "Character Movement/Location/Z Axis"); }
                set { Write(Eq2Process, "Character Movement/Location/Z Axis", value); }
            }

            public float CharacterSpeed
            {
                get
                {
                    var addr = this.Resolve(Eq2Process, "Character Movement/Speed");
                    return Eq2Process.Read<float>(addr);

                    //return Read<float>(Eq2Process, "Character Movement/Speed"); 


                }
                set { Write(Eq2Process, "Character Movement/Speed", value); }
            }

            public bool CharacterRunning
            {
                get { return Read<byte>(Eq2Process, "Character Movement/Toggle/Running") == 1; }
                set { Write(Eq2Process, "Character Movement/Toggle/Running", value ? 1 : 0); }
            }

            public string CharacterName
            {
                get { return ReadString(Eq2Process, "Character Data/Struct/Name", 255, Encoding.ASCII, true); }
            }

            public string CharacterSurname
            {
                get { return ReadString(Eq2Process, "Character Data/Struct/Surname", 255, Encoding.ASCII, true); }
            }

            public int CharacterCurrentHealth
            {
                get { return Read<int>(Eq2Process, "Character Data/Struct/Current Health"); }
            }

            public int CharacterMaxHealth
            {
                get { return Read<int>(Eq2Process, "Character Data/Struct/Max Health"); }
            }

            public int CharacterHealthPercent
            {
                get
                {
                    return (int)(((float)CharacterCurrentHealth / (float)CharacterMaxHealth) * 100);
                }
            }

            public int CharacterCurrentPower
            {
                get { return Read<int>(Eq2Process, "Character Data/Struct/Current Power"); }
            }

            public int CharacterMaxPower
            {
                get { return Read<int>(Eq2Process, "Character Data/Struct/Max Power"); }
            }

            public int CharacterPowerPercent
            {
                get
                {
                    return (int)(((float)CharacterCurrentPower / (float)CharacterMaxPower) * 100);
                }
            }

            public bool CharacterHasTarget
            {
                get { return TargetSpawnId != -1; }
            }

            public int CharacterLevel
            {
                get { return (int)Read<byte>(Eq2Process, "Character Level"); }
            }

            public string CharacterClassName
            {
                get { return ReadString(Eq2Process, "Class Data/Class Name", 255, Encoding.ASCII, true); }
            }

            public string CharacterServerName
            {
                get { return ReadString(Eq2Process, "Server Data/Server Name", 255, Encoding.ASCII, true); }
            }

            public string CharacterZoneName
            {
                get { return ReadString(Eq2Process, "Zone Data/Zone Name", 255, Encoding.ASCII, true); }
            }

            public bool CharacterIsAutoRunning
            {
                get
                {
                    var x = Resolve(Eq2Process, "Camera And Movement/Autorun");
                    return Read<byte>(Eq2Process, "Camera And Movement/Autorun") == 1;

                }
                set { Write<byte>(Eq2Process, "Camera And Movement/Autorun", (byte)(value ? 1 : 0)); }
            }

            public bool CharacterIsFighting
            {
                get { return Read<byte>(Eq2Process, "Camera And Movement/Fighting") == 1; }
            }

            public float CharacterHeading
            {
                get { return Read<float>(Eq2Process, "Camera And Movement/Camera Details/Character Heading").Wrap(360, false); }
                set { Write(Eq2Process, "Camera And Movement/Camera Details/Character Heading", value.Wrap(360, false)); }
            }

            #endregion

            #region Target

            public int TargetSpawnId
            {
                get { return Read<int>(Eq2Process, "Target Data/Spawn ID"); }
            }

            public int TargetSpawnId2
            {
                get { return Read<int>(Eq2Process, "Target Location/Spawn ID"); }
            }

            public Vector3 TargetLocation
            {
                get { return Read<Vector3>(Eq2Process, "Target Location/X Axis"); }
            }

            public float TargetDistance
            {
                get { return Read<float>(Eq2Process, "Target Location/Distance"); }
            }

            public float TargetX
            {
                get { return Read<float>(Eq2Process, "Target Location/X Axis"); }
            }

            public float TargetY
            {
                get { return Read<float>(Eq2Process, "Target Location/Y Axis"); }
            }

            public float TargetZ
            {
                get { return Read<float>(Eq2Process, "Target Location/Z Axis"); }
            }

            public int TargetHealth
            {
                get { return (int)Read<byte>(Eq2Process, "Target Data/Health"); }
            }

            public int TargetAssistHealth
            {
                get { return (int)Read<byte>(Eq2Process, "Target Data/Assist Health"); }
            }

            public int TargetPower
            {
                get { return (int)Read<byte>(Eq2Process, "Target Data/Power"); }
            }

            public int TargetLevel
            {
                get { return (int)Read<byte>(Eq2Process, "Target Data/Level"); }
            }

            #endregion

            #region Eq2

            public bool Eq2HasChatWindowFocus
            {
                get { return Read<int>(Eq2Process, "Chat Window Input Pointer") != 0; }
            }

            public int Eq2CursorIconId
            {
                get { return Read<int>(Eq2Process, "Cursor Icon ID"); }
            }

            #endregion

            #region Camera

            public float CameraZoom
            {
                get { return Read<float>(Eq2Process, "Camera And Movement/Zoom"); }
                set { Write(Eq2Process, "Camera And Movement/Zoom", value); }
            }

            public float CameraYaw
            {
                get { return Read<float>(Eq2Process, "Camera And Movement/Camera Details/Camera Yaw"); }
                set { Write(Eq2Process, "Camera And Movement/Camera Details/Camera Yaw", value); }
            }

            public float CameraPitch
            {
                get { return Read<float>(Eq2Process, "Camera And Movement/Camera Details/Camera Pitch"); }
                set { Write(Eq2Process, "Camera And Movement/Camera Details/Camera Pitch", value); }
            }

            public Camera Camera
            {
                get { return new Camera(CameraPitch, CameraYaw, CameraZoom); }
                set
                {
                    CameraYaw = value.Yaw;
                    CameraPitch = value.Pitch;
                    CameraZoom = value.Zoom;
                }
            }


            #endregion

            #region Autocrat

            public IntPtr AutocratTargetNameFunction
            {
                get { return Resolve(Eq2Process, "Target Name Function Location"); }
            }

            public IntPtr AutocratPCTargetFunction
            {
                get { return Resolve(Eq2Process, "PC Target Name Function"); }
            }

            public IntPtr AutocratNPCTargetFunction
            {
                get { return Resolve(Eq2Process, "NPC Target Name Function"); }
            }

            #endregion

        }
    }

    public abstract class Accessor
    {
        internal Eq2 Parent { get; set; }
    }

    public class CharacterAccessor : Accessor
    {
        public Vector3 Location
        {
            get { return Parent.eq2Pointers.CharacterLocation; }
            set { Parent.eq2Pointers.CharacterLocation = value; }
        }

        public float X
        {
            get { return Parent.eq2Pointers.CharacterX; }
            set { Parent.eq2Pointers.CharacterX = value; }
        }

        public float Y
        {
            get { return Parent.eq2Pointers.CharacterY; }
            set { Parent.eq2Pointers.CharacterY = value; }
        }

        public float Z
        {
            get { return Parent.eq2Pointers.CharacterZ; }
            set { Parent.eq2Pointers.CharacterZ = value; }
        }

        public float Speed
        {
            get { return Parent.eq2Pointers.CharacterSpeed; }
            set { Parent.eq2Pointers.CharacterSpeed = value; }
        }

        public bool Running
        {
            get { return Parent.eq2Pointers.CharacterRunning; }
            set { Parent.eq2Pointers.CharacterRunning = value; }
        }

        public string Name
        {
            get { return Parent.eq2Pointers.CharacterName; }
        }

        public string Surname
        {
            get { return Parent.eq2Pointers.CharacterSurname; }
        }

        public int CurrentHealth
        {
            get { return Parent.eq2Pointers.CharacterCurrentHealth; }
        }

        public int MaxHealth
        {
            get { return Parent.eq2Pointers.CharacterMaxHealth; }
        }

        public int HealthPercent
        {
            get { return Parent.eq2Pointers.CharacterHealthPercent; }
        }

        public int CurrentPower
        {
            get { return Parent.eq2Pointers.CharacterCurrentPower; }
        }

        public int MaxPower
        {
            get { return Parent.eq2Pointers.CharacterMaxPower; }
        }

        public int PowerPercent
        {
            get { return Parent.eq2Pointers.CharacterPowerPercent; }
        }

        public bool HasTarget
        {
            get { return Parent.eq2Pointers.CharacterHasTarget; }
        }

        public int Level
        {
            get { return Parent.eq2Pointers.CharacterLevel; }
        }

        public string ClassName
        {
            get { return Parent.eq2Pointers.CharacterClassName; }
        }

        public string ServerName
        {
            get { return Parent.eq2Pointers.CharacterServerName; }
        }

        public string ZoneName
        {
            get { return Parent.eq2Pointers.CharacterZoneName; }
        }

        public bool IsAutoRunning
        {
            get { return Parent.eq2Pointers.CharacterIsAutoRunning; }
            set { Parent.eq2Pointers.CharacterIsAutoRunning = value; }
        }

        public bool IsFighting
        {
            get { return Parent.eq2Pointers.CharacterIsFighting; }
        }

        public float Heading
        {
            get { return Parent.eq2Pointers.CharacterHeading; }
            set { Parent.eq2Pointers.CharacterHeading = value; }
        }


    }

    public class TargetAccessor : Accessor
    {
        public int SpawnId
        {
            get { return Parent.eq2Pointers.TargetSpawnId; }
        }
        public int SpawnId2
        {
            get { return Parent.eq2Pointers.TargetSpawnId2; }
        }

        public Vector3 Location
        {
            get { return Parent.eq2Pointers.TargetLocation; }
        }

        public float Distance
        {
            get { return Parent.eq2Pointers.TargetDistance; }
        }

        public float X
        {
            get { return Parent.eq2Pointers.TargetX; }
        }

        public float Y
        {
            get { return Parent.eq2Pointers.TargetY; }
        }

        public float Z
        {
            get { return Parent.eq2Pointers.TargetZ; }
        }

        public int Health
        {
            get { return Parent.eq2Pointers.TargetHealth; }
        }

        public int AssistHealth
        {
            get { return Parent.eq2Pointers.TargetAssistHealth; }
        }

        public int Power
        {
            get { return Parent.eq2Pointers.TargetPower; }
        }

        public int Level
        {
            get { return Parent.eq2Pointers.TargetLevel; }
        }


    }

    public class UIAccessor : Accessor
    {
        public bool HasChatWindowFocus
        {
            get { return Parent.eq2Pointers.Eq2HasChatWindowFocus; }
        }

        public int CursorIconId
        {
            get { return Parent.eq2Pointers.Eq2CursorIconId; }
        }
    }

    public class CameraAccessor : Accessor
    {
        public float Zoom
        {
            get { return Parent.eq2Pointers.CameraZoom; }
            set { Parent.eq2Pointers.CameraZoom = value; }
        }

        public float Yaw
        {
            get { return Parent.eq2Pointers.CameraYaw; }
            set { Parent.eq2Pointers.CameraYaw = value; }
        }

        public float Pitch
        {
            get { return Parent.eq2Pointers.CameraPitch; }
            set { Parent.eq2Pointers.CameraPitch = value; }
        }

        public Camera Camera
        {
            get { return Parent.eq2Pointers.Camera; }
            set { Parent.eq2Pointers.Camera = value; }
        }

    }

    public class AutocratAccessor : Accessor
    {
        public IntPtr TargetNameFunction
        {
            get { return Parent.eq2Pointers.AutocratTargetNameFunction; }
        }

        public IntPtr PCTargetFunction
        {
            get { return Parent.eq2Pointers.AutocratPCTargetFunction; }
        }

        public IntPtr NPCTargetFunction
        {
            get { return Parent.eq2Pointers.AutocratNPCTargetFunction; }
        }
    }
}
