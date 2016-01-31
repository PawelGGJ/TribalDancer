using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class BeatEngine : MonoBehaviour
{

    public static BeatEngine instance;
   [SerializeField]
   AudioSource theSong;
   [SerializeField]
   Image theFrame;
   [Range(1f, 1999f)]
   [SerializeField]
   float offsetMillsec = 400f; //number must be between 0 and 2000
   [SerializeField]
   Image preparationImage;
   [SerializeField]
   Sprite pic1;
   [SerializeField]
   Sprite pic2;
   [SerializeField]
   Sprite pic3;
   [SerializeField]
   Sprite pic4;
   [SerializeField]
   GameObject symbol0;
   [SerializeField]
   GameObject symbol1;
   [SerializeField]
   GameObject symbol2;
   [SerializeField]
   GameObject symbol3;
   [SerializeField]
   GameObject symbol4;
   [SerializeField]
   GameObject symbol5;
   [SerializeField]
   GameObject symbol6;
   [SerializeField]
   GameObject symbol7;
   [SerializeField]
   AudioSource combo1;
   [SerializeField]
   AudioSource combo2;
   [SerializeField]
   AudioSource combo3;
   [SerializeField]
   AudioSource combo4;

   private GameObject currentlyVisible;

   bool startDancing;
   int realTimer;
   float microTimer;
   bool resetMicro;
   bool microStep1;
   bool microStep2;
   bool microStep3;

   DateTime startTime;
   DateTime microStartTime;
   TimeSpan timeSpan;

   float timePerBeat;
   float goodHitx2;
   float perfectHit;
   float badHit;

   bool hasMatched;

   private int[] currBeatSeq = new int[4];

   [SerializeField]
   List<int> diffIncrAt = new List<int>();
   private int beatCounter;

   [SerializeField]
   Text theScore;
   private int currPoints;
   public bool instructMode;
   private Vector3 hmTop;
   private Vector3 hmBot;

   private int currDifficulty;
   [SerializeField]
   List<int> sequencesPerDifficulty = new List<int>();
   private int seqPlayed;
   private int comboCntr;
   private int bonusPts;
   [SerializeField]
   List<Vector2> comboBonusList = new List<Vector2>();
   private int currReachedCombo;

   int[][] diff0 = {
            new int[] {0,8,0,8},
            new int[] {1,8,1,8},
            new int[] {2,8,2,8},
            new int[] {3,8,3,8},
            new int[] {8,0,0,8},
            new int[] {8,1,1,8},
            new int[] {8,2,2,8},
            new int[] {8,3,3,8}
        };

   int[][] diff1 = {
            new int[] {4,8,4,8},
            new int[] {5,8,5,8},
            new int[] {6,8,6,8},
            new int[] {7,8,7,8},
            new int[] {8,4,4,8},
            new int[] {8,5,5,8},
            new int[] {8,6,6,8},
            new int[] {8,7,7,8}
        };

   int[][] diff2 = {
            new int[] {0,0,0,0},
            new int[] {1,1,1,1},
            new int[] {2,2,2,2},
            new int[] {3,3,3,3},
            new int[] {4,4,4,4},
            new int[] {5,5,5,5},
            new int[] {6,6,6,6},
            new int[] {7,7,7,7}
        };

   int[][] diff3 = {
            new int[] {0,0,0,1},
            new int[] {1,1,1,2},
            new int[] {2,2,2,3},
            new int[] {3,3,3,0},
            new int[] {0,0,0,2},
            new int[] {1,1,1,3},
            new int[] {2,2,2,0},
            new int[] {3,3,3,1},
            new int[] {0,0,0,3},
            new int[] {1,1,1,0},
            new int[] {2,2,2,1},
            new int[] {3,3,3,2},
            new int[] {0,0,1,0},
            new int[] {1,1,2,1},
            new int[] {2,2,3,2},
            new int[] {3,3,0,3},
            new int[] {0,0,2,0},
            new int[] {1,1,3,1},
            new int[] {2,2,0,2},
            new int[] {3,3,1,3},
            new int[] {0,0,3,0},
            new int[] {1,1,0,1},
            new int[] {2,2,1,2},
            new int[] {3,3,2,3},
            new int[] {0,1,0,0},
            new int[] {1,2,1,1},
            new int[] {2,3,2,2},
            new int[] {3,0,3,3},
            new int[] {0,2,0,0},
            new int[] {1,3,1,1},
            new int[] {2,0,2,2},
            new int[] {3,1,3,3},
            new int[] {3,0,0,0},
            new int[] {0,1,1,1},
            new int[] {1,2,2,2},
            new int[] {2,3,3,3},
            new int[] {1,0,0,0},
            new int[] {2,1,1,1},
            new int[] {3,2,2,2},
            new int[] {0,3,3,3},
            new int[] {2,0,0,0},
            new int[] {3,1,1,1},
            new int[] {0,2,2,2},
            new int[] {1,3,3,3},
            new int[] {3,0,0,0},
            new int[] {0,1,1,1},
            new int[] {1,2,2,2},
            new int[] {2,3,3,3}
};

   int[][] diff4 = {
            new int[] {4,4,4,5},
            new int[] {5,5,5,6},
            new int[] {6,6,6,7},
            new int[] {7,7,7,4},
            new int[] {4,4,4,6},
            new int[] {5,5,5,7},
            new int[] {6,6,6,4},
            new int[] {7,7,7,5},
            new int[] {4,4,4,7},
            new int[] {5,5,5,4},
            new int[] {6,6,6,5},
            new int[] {7,7,7,6},
            new int[] {4,4,5,4},
            new int[] {5,5,6,5},
            new int[] {6,6,7,6},
            new int[] {7,7,4,7},
            new int[] {4,4,6,4},
            new int[] {5,5,7,5},
            new int[] {6,6,4,6},
            new int[] {7,7,5,7},
            new int[] {4,4,7,4},
            new int[] {5,5,4,5},
            new int[] {6,6,5,6},
            new int[] {7,7,6,7},
            new int[] {4,5,4,4},
            new int[] {5,6,5,5},
            new int[] {6,7,6,6},
            new int[] {7,4,7,7},
            new int[] {4,6,4,4},
            new int[] {5,7,5,5},
            new int[] {6,4,6,6},
            new int[] {7,5,7,7},
            new int[] {7,4,4,4},
            new int[] {4,5,5,5},
            new int[] {5,6,6,6},
            new int[] {6,7,7,7},
            new int[] {5,4,4,4},
            new int[] {6,5,5,5},
            new int[] {7,6,6,6},
            new int[] {4,7,7,7},
            new int[] {6,4,4,4},
            new int[] {7,5,5,5},
            new int[] {4,6,6,6},
            new int[] {5,7,7,7},
            new int[] {7,4,4,4},
            new int[] {4,5,5,5},
            new int[] {5,6,6,6},
            new int[] {6,7,7,7}
};

   int[][] diff5 = {
            new int[] {0,1,1,0},
            new int[] {1,2,2,1},
            new int[] {2,3,3,2},
            new int[] {3,0,0,3},
            new int[] {0,2,2,0},
            new int[] {1,3,3,1},
            new int[] {2,0,0,2},
            new int[] {3,1,1,3},
            new int[] {0,3,3,0},
            new int[] {1,0,0,1},
            new int[] {2,1,1,2},
            new int[] {3,2,2,3},
            new int[] {1,0,0,1},
            new int[] {2,1,1,2},
            new int[] {3,2,2,3},
            new int[] {0,3,3,0},
            new int[] {2,0,0,2},
            new int[] {3,1,1,3},
            new int[] {0,2,2,0},
            new int[] {1,3,3,1},
            new int[] {3,0,0,3},
};

   int[][] diff6 = {
            new int[] {0,1,0,1},
            new int[] {1,2,1,2},
            new int[] {2,3,2,3},
            new int[] {3,0,3,0},
            new int[] {0,2,0,2},
            new int[] {1,3,1,3},
            new int[] {2,0,2,0},
            new int[] {3,1,3,1},
            new int[] {0,3,0,3},
            new int[] {1,0,1,0},
            new int[] {2,1,2,1},
            new int[] {3,2,3,2},
            new int[] {1,0,1,0},
            new int[] {2,1,2,1},
            new int[] {3,2,3,2},
            new int[] {0,3,0,3},
            new int[] {2,0,2,0},
            new int[] {3,1,3,1},
            new int[] {0,2,0,2},
            new int[] {1,3,1,3},
            new int[] {3,0,3,0},
};

   int[][] diff7 = {
            new int[] {4,5,5,4},
            new int[] {5,6,6,5},
            new int[] {6,7,7,6},
            new int[] {7,4,4,7},
            new int[] {4,6,6,4},
            new int[] {5,7,7,5},
            new int[] {6,4,4,6},
            new int[] {7,5,5,7},
            new int[] {4,7,7,4},
            new int[] {5,4,4,5},
            new int[] {6,5,5,6},
            new int[] {7,6,6,7},
            new int[] {5,4,4,5},
            new int[] {6,5,5,6},
            new int[] {7,6,6,7},
            new int[] {4,7,7,4},
            new int[] {6,4,4,6},
            new int[] {7,5,5,7},
            new int[] {4,6,6,4},
            new int[] {5,7,7,5},
            new int[] {7,4,4,7},
            new int[] {4,5,5,4},
            new int[] {5,6,6,5},
            new int[] {6,7,7,6}
};

   int[][] diff8 = {
            new int[] {4,5,4,5},
            new int[] {5,6,5,6},
            new int[] {6,7,6,7},
            new int[] {7,4,7,4},
            new int[] {4,6,4,6},
            new int[] {5,7,5,7},
            new int[] {6,4,6,4},
            new int[] {7,5,7,5},
            new int[] {4,7,4,7},
            new int[] {5,4,5,4},
            new int[] {6,5,6,5},
            new int[] {7,6,7,6},
            new int[] {5,4,5,4},
            new int[] {6,5,6,5},
            new int[] {7,6,7,6},
            new int[] {4,7,4,7},
            new int[] {6,4,6,4},
            new int[] {7,5,7,5},
            new int[] {4,6,4,6},
            new int[] {5,7,5,7},
            new int[] {7,4,7,4},
            new int[] {4,5,4,5},
            new int[] {5,6,5,6},
            new int[] {6,7,6,7}
};

   int[][] diff9 = {
            new int[] {0,1,2,0},
            new int[] {1,2,3,1},
            new int[] {2,3,0,2},
            new int[] {3,0,1,3},
            new int[] {3,1,1,2},
            new int[] {0,2,2,3},
            new int[] {0,1,2,0},
            new int[] {1,2,3,1},
            new int[] {2,3,0,2},
            new int[] {3,0,1,3},
            new int[] {2,0,0,1},
            new int[] {3,1,1,2},
            new int[] {0,2,2,3},
            new int[] {1,3,3,0},
            new int[] {0,0,1,2},
            new int[] {1,1,2,3},
            new int[] {2,2,3,0},
            new int[] {3,3,0,1},
            new int[] {1,2,0,0},
            new int[] {2,3,1,1},
            new int[] {3,0,2,2},
            new int[] {0,1,3,3}
};

   int[][] diff10 = {
            new int[] {4,5,6,4},
            new int[] {5,6,7,5},
            new int[] {6,7,4,6},
            new int[] {7,4,5,7},
            new int[] {7,5,5,6},
            new int[] {4,6,6,7},
            new int[] {4,5,6,4},
            new int[] {5,6,7,5},
            new int[] {6,7,4,6},
            new int[] {7,4,5,7},
            new int[] {6,4,4,5},
            new int[] {7,5,5,6},
            new int[] {4,6,6,7},
            new int[] {5,7,7,4},
            new int[] {4,4,5,6},
            new int[] {5,5,6,7},
            new int[] {6,6,7,4},
            new int[] {7,7,4,5},
            new int[] {5,6,4,4},
            new int[] {6,7,5,5},
            new int[] {7,4,6,6},
            new int[] {4,5,7,7}
};

   int[][] diff11 = {
            new int[] {0,1,2,3},
            new int[] {0,1,2,4},
            new int[] {0,1,2,5},
            new int[] {0,1,2,6},
            new int[] {0,1,2,7},
            new int[] {0,1,3,2},
            new int[] {0,1,3,4},
            new int[] {0,1,3,5},
            new int[] {0,1,3,6},
            new int[] {0,1,3,7},
            new int[] {0,1,4,2},
            new int[] {0,1,4,3},
            new int[] {0,1,4,5},
            new int[] {0,1,4,6},
            new int[] {0,1,4,7},
            new int[] {0,1,5,2},
            new int[] {0,1,5,3},
            new int[] {0,1,5,4},
            new int[] {0,1,5,6},
            new int[] {0,1,5,7},
            new int[] {0,1,6,2},
            new int[] {0,1,6,3},
            new int[] {0,1,6,4},
            new int[] {0,1,6,5},
            new int[] {0,1,6,7},
            new int[] {0,1,7,2},
            new int[] {0,1,7,3},
            new int[] {0,1,7,4},
            new int[] {0,1,7,5},
            new int[] {0,1,7,6},
            new int[] {0,2,1,3},
            new int[] {0,2,1,4},
            new int[] {0,2,1,5},
            new int[] {0,2,1,6},
            new int[] {0,2,1,7},
            new int[] {0,2,3,1},
            new int[] {0,2,3,4},
            new int[] {0,2,3,5},
            new int[] {0,2,3,6},
            new int[] {0,2,3,7},
            new int[] {0,2,4,1},
            new int[] {0,2,4,3},
            new int[] {0,2,4,5},
            new int[] {0,2,4,6},
            new int[] {0,2,4,7},
            new int[] {0,2,5,1},
            new int[] {0,2,5,3},
            new int[] {0,2,5,4},
            new int[] {0,2,5,6},
            new int[] {0,2,5,7},
            new int[] {0,2,6,1},
            new int[] {0,2,6,3},
            new int[] {0,2,6,4},
            new int[] {0,2,6,5},
            new int[] {0,2,6,7},
            new int[] {0,2,7,1},
            new int[] {0,2,7,3},
            new int[] {0,2,7,4},
            new int[] {0,2,7,5},
            new int[] {0,2,7,6},
            new int[] {0,3,1,2},
            new int[] {0,3,1,4},
            new int[] {0,3,1,5},
            new int[] {0,3,1,6},
            new int[] {0,3,1,7},
            new int[] {0,3,2,1},
            new int[] {0,3,2,4},
            new int[] {0,3,2,5},
            new int[] {0,3,2,6},
            new int[] {0,3,2,7},
            new int[] {0,3,4,1},
            new int[] {0,3,4,2},
            new int[] {0,3,4,5},
            new int[] {0,3,4,6},
            new int[] {0,3,4,7},
            new int[] {0,3,5,1},
            new int[] {0,3,5,2},
            new int[] {0,3,5,4},
            new int[] {0,3,5,6},
            new int[] {0,3,5,7},
            new int[] {0,3,6,1},
            new int[] {0,3,6,2},
            new int[] {0,3,6,4},
            new int[] {0,3,6,5},
            new int[] {0,3,6,7},
            new int[] {0,3,7,1},
            new int[] {0,3,7,2},
            new int[] {0,3,7,4},
            new int[] {0,3,7,5},
            new int[] {0,3,7,6},
            new int[] {0,4,1,2},
            new int[] {0,4,1,3},
            new int[] {0,4,1,5},
            new int[] {0,4,1,6},
            new int[] {0,4,1,7},
            new int[] {0,4,2,1},
            new int[] {0,4,2,3},
            new int[] {0,4,2,5},
            new int[] {0,4,2,6},
            new int[] {0,4,2,7},
            new int[] {0,4,3,1},
            new int[] {0,4,3,2},
            new int[] {0,4,3,5},
            new int[] {0,4,3,6},
            new int[] {0,4,3,7},
            new int[] {0,4,5,1},
            new int[] {0,4,5,2},
            new int[] {0,4,5,3},
            new int[] {0,4,5,6},
            new int[] {0,4,5,7},
            new int[] {0,4,6,1},
            new int[] {0,4,6,2},
            new int[] {0,4,6,3},
            new int[] {0,4,6,5},
            new int[] {0,4,6,7},
            new int[] {0,4,7,1},
            new int[] {0,4,7,2},
            new int[] {0,4,7,3},
            new int[] {0,4,7,5},
            new int[] {0,4,7,6},
            new int[] {0,5,1,2},
            new int[] {0,5,1,3},
            new int[] {0,5,1,4},
            new int[] {0,5,1,6},
            new int[] {0,5,1,7},
            new int[] {0,5,2,1},
            new int[] {0,5,2,3},
            new int[] {0,5,2,4},
            new int[] {0,5,2,6},
            new int[] {0,5,2,7},
            new int[] {0,5,3,1},
            new int[] {0,5,3,2},
            new int[] {0,5,3,4},
            new int[] {0,5,3,6},
            new int[] {0,5,3,7},
            new int[] {0,5,4,1},
            new int[] {0,5,4,2},
            new int[] {0,5,4,3},
            new int[] {0,5,4,6},
            new int[] {0,5,4,7},
            new int[] {0,5,6,1},
            new int[] {0,5,6,2},
            new int[] {0,5,6,3},
            new int[] {0,5,6,4},
            new int[] {0,5,6,7},
            new int[] {0,5,7,1},
            new int[] {0,5,7,2},
            new int[] {0,5,7,3},
            new int[] {0,5,7,4},
            new int[] {0,5,7,6},
            new int[] {0,6,1,2},
            new int[] {0,6,1,3},
            new int[] {0,6,1,4},
            new int[] {0,6,1,5},
            new int[] {0,6,1,7},
            new int[] {0,6,2,1},
            new int[] {0,6,2,3},
            new int[] {0,6,2,4},
            new int[] {0,6,2,5},
            new int[] {0,6,2,7},
            new int[] {0,6,3,1},
            new int[] {0,6,3,2},
            new int[] {0,6,3,4},
            new int[] {0,6,3,5},
            new int[] {0,6,3,7},
            new int[] {0,6,4,1},
            new int[] {0,6,4,2},
            new int[] {0,6,4,3},
            new int[] {0,6,4,5},
            new int[] {0,6,4,7},
            new int[] {0,6,5,1},
            new int[] {0,6,5,2},
            new int[] {0,6,5,3},
            new int[] {0,6,5,4},
            new int[] {0,6,5,7},
            new int[] {0,6,7,1},
            new int[] {0,6,7,2},
            new int[] {0,6,7,3},
            new int[] {0,6,7,4},
            new int[] {0,6,7,5},
            new int[] {0,7,1,2},
            new int[] {0,7,1,3},
            new int[] {0,7,1,4},
            new int[] {0,7,1,5},
            new int[] {0,7,1,6},
            new int[] {0,7,2,1},
            new int[] {0,7,2,3},
            new int[] {0,7,2,4},
            new int[] {0,7,2,5},
            new int[] {0,7,2,6},
            new int[] {0,7,3,1},
            new int[] {0,7,3,2},
            new int[] {0,7,3,4},
            new int[] {0,7,3,5},
            new int[] {0,7,3,6},
            new int[] {0,7,4,1},
            new int[] {0,7,4,2},
            new int[] {0,7,4,3},
            new int[] {0,7,4,5},
            new int[] {0,7,4,6},
            new int[] {0,7,5,1},
            new int[] {0,7,5,2},
            new int[] {0,7,5,3},
            new int[] {0,7,5,4},
            new int[] {0,7,5,6},
            new int[] {0,7,6,1},
            new int[] {0,7,6,2},
            new int[] {0,7,6,3},
            new int[] {0,7,6,4},
            new int[] {0,7,6,5},
            new int[] {1,0,2,3},
            new int[] {1,0,2,4},
            new int[] {1,0,2,5},
            new int[] {1,0,2,6},
            new int[] {1,0,2,7},
            new int[] {1,0,3,2},
            new int[] {1,0,3,4},
            new int[] {1,0,3,5},
            new int[] {1,0,3,6},
            new int[] {1,0,3,7},
            new int[] {1,0,4,2},
            new int[] {1,0,4,3},
            new int[] {1,0,4,5},
            new int[] {1,0,4,6},
            new int[] {1,0,4,7},
            new int[] {1,0,5,2},
            new int[] {1,0,5,3},
            new int[] {1,0,5,4},
            new int[] {1,0,5,6},
            new int[] {1,0,5,7},
            new int[] {1,0,6,2},
            new int[] {1,0,6,3},
            new int[] {1,0,6,4},
            new int[] {1,0,6,5},
            new int[] {1,0,6,7},
            new int[] {1,0,7,2},
            new int[] {1,0,7,3},
            new int[] {1,0,7,4},
            new int[] {1,0,7,5},
            new int[] {1,0,7,6},
            new int[] {1,2,0,3},
            new int[] {1,2,0,4},
            new int[] {1,2,0,5},
            new int[] {1,2,0,6},
            new int[] {1,2,0,7},
            new int[] {1,2,3,0},
            new int[] {1,2,3,4},
            new int[] {1,2,3,5},
            new int[] {1,2,3,6},
            new int[] {1,2,3,7},
            new int[] {1,2,4,0},
            new int[] {1,2,4,3},
            new int[] {1,2,4,5},
            new int[] {1,2,4,6},
            new int[] {1,2,4,7},
            new int[] {1,2,5,0},
            new int[] {1,2,5,3},
            new int[] {1,2,5,4},
            new int[] {1,2,5,6},
            new int[] {1,2,5,7},
            new int[] {1,2,6,0},
            new int[] {1,2,6,3},
            new int[] {1,2,6,4},
            new int[] {1,2,6,5},
            new int[] {1,2,6,7},
            new int[] {1,2,7,0},
            new int[] {1,2,7,3},
            new int[] {1,2,7,4},
            new int[] {1,2,7,5},
            new int[] {1,2,7,6},
            new int[] {1,3,0,2},
            new int[] {1,3,0,4},
            new int[] {1,3,0,5},
            new int[] {1,3,0,6},
            new int[] {1,3,0,7},
            new int[] {1,3,2,0},
            new int[] {1,3,2,4},
            new int[] {1,3,2,5},
            new int[] {1,3,2,6},
            new int[] {1,3,2,7},
            new int[] {1,3,4,0},
            new int[] {1,3,4,2},
            new int[] {1,3,4,5},
            new int[] {1,3,4,6},
            new int[] {1,3,4,7},
            new int[] {1,3,5,0},
            new int[] {1,3,5,2},
            new int[] {1,3,5,4},
            new int[] {1,3,5,6},
            new int[] {1,3,5,7},
            new int[] {1,3,6,0},
            new int[] {1,3,6,2},
            new int[] {1,3,6,4},
            new int[] {1,3,6,5},
            new int[] {1,3,6,7},
            new int[] {1,3,7,0},
            new int[] {1,3,7,2},
            new int[] {1,3,7,4},
            new int[] {1,3,7,5},
            new int[] {1,3,7,6},
            new int[] {1,4,0,2},
            new int[] {1,4,0,3},
            new int[] {1,4,0,5},
            new int[] {1,4,0,6},
            new int[] {1,4,0,7},
            new int[] {1,4,2,0},
            new int[] {1,4,2,3},
            new int[] {1,4,2,5},
            new int[] {1,4,2,6},
            new int[] {1,4,2,7},
            new int[] {1,4,3,0},
            new int[] {1,4,3,2},
            new int[] {1,4,3,5},
            new int[] {1,4,3,6},
            new int[] {1,4,3,7},
            new int[] {1,4,5,0},
            new int[] {1,4,5,2},
            new int[] {1,4,5,3},
            new int[] {1,4,5,6},
            new int[] {1,4,5,7},
            new int[] {1,4,6,0},
            new int[] {1,4,6,2},
            new int[] {1,4,6,3},
            new int[] {1,4,6,5},
            new int[] {1,4,6,7},
            new int[] {1,4,7,0},
            new int[] {1,4,7,2},
            new int[] {1,4,7,3},
            new int[] {1,4,7,5},
            new int[] {1,4,7,6},
            new int[] {1,5,0,2},
            new int[] {1,5,0,3},
            new int[] {1,5,0,4},
            new int[] {1,5,0,6},
            new int[] {1,5,0,7},
            new int[] {1,5,2,0},
            new int[] {1,5,2,3},
            new int[] {1,5,2,4},
            new int[] {1,5,2,6},
            new int[] {1,5,2,7},
            new int[] {1,5,3,0},
            new int[] {1,5,3,2},
            new int[] {1,5,3,4},
            new int[] {1,5,3,6},
            new int[] {1,5,3,7},
            new int[] {1,5,4,0},
            new int[] {1,5,4,2},
            new int[] {1,5,4,3},
            new int[] {1,5,4,6},
            new int[] {1,5,4,7},
            new int[] {1,5,6,0},
            new int[] {1,5,6,2},
            new int[] {1,5,6,3},
            new int[] {1,5,6,4},
            new int[] {1,5,6,7},
            new int[] {1,5,7,0},
            new int[] {1,5,7,2},
            new int[] {1,5,7,3},
            new int[] {1,5,7,4},
            new int[] {1,5,7,6},
            new int[] {1,6,0,2},
            new int[] {1,6,0,3},
            new int[] {1,6,0,4},
            new int[] {1,6,0,5},
            new int[] {1,6,0,7},
            new int[] {1,6,2,0},
            new int[] {1,6,2,3},
            new int[] {1,6,2,4},
            new int[] {1,6,2,5},
            new int[] {1,6,2,7},
            new int[] {1,6,3,0},
            new int[] {1,6,3,2},
            new int[] {1,6,3,4},
            new int[] {1,6,3,5},
            new int[] {1,6,3,7},
            new int[] {1,6,4,0},
            new int[] {1,6,4,2},
            new int[] {1,6,4,3},
            new int[] {1,6,4,5},
            new int[] {1,6,4,7},
            new int[] {1,6,5,0},
            new int[] {1,6,5,2},
            new int[] {1,6,5,3},
            new int[] {1,6,5,4},
            new int[] {1,6,5,7},
            new int[] {1,6,7,0},
            new int[] {1,6,7,2},
            new int[] {1,6,7,3},
            new int[] {1,6,7,4},
            new int[] {1,6,7,5},
            new int[] {1,7,0,2},
            new int[] {1,7,0,3},
            new int[] {1,7,0,4},
            new int[] {1,7,0,5},
            new int[] {1,7,0,6},
            new int[] {1,7,2,0},
            new int[] {1,7,2,3},
            new int[] {1,7,2,4},
            new int[] {1,7,2,5},
            new int[] {1,7,2,6},
            new int[] {1,7,3,0},
            new int[] {1,7,3,2},
            new int[] {1,7,3,4},
            new int[] {1,7,3,5},
            new int[] {1,7,3,6},
            new int[] {1,7,4,0},
            new int[] {1,7,4,2},
            new int[] {1,7,4,3},
            new int[] {1,7,4,5},
            new int[] {1,7,4,6},
            new int[] {1,7,5,0},
            new int[] {1,7,5,2},
            new int[] {1,7,5,3},
            new int[] {1,7,5,4},
            new int[] {1,7,5,6},
            new int[] {1,7,6,0},
            new int[] {1,7,6,2},
            new int[] {1,7,6,3},
            new int[] {1,7,6,4},
            new int[] {1,7,6,5},
            new int[] {2,0,1,3},
            new int[] {2,0,1,4},
            new int[] {2,0,1,5},
            new int[] {2,0,1,6},
            new int[] {2,0,1,7},
            new int[] {2,0,3,1},
            new int[] {2,0,3,4},
            new int[] {2,0,3,5},
            new int[] {2,0,3,6},
            new int[] {2,0,3,7},
            new int[] {2,0,4,1},
            new int[] {2,0,4,3},
            new int[] {2,0,4,5},
            new int[] {2,0,4,6},
            new int[] {2,0,4,7},
            new int[] {2,0,5,1},
            new int[] {2,0,5,3},
            new int[] {2,0,5,4},
            new int[] {2,0,5,6},
            new int[] {2,0,5,7},
            new int[] {2,0,6,1},
            new int[] {2,0,6,3},
            new int[] {2,0,6,4},
            new int[] {2,0,6,5},
            new int[] {2,0,6,7},
            new int[] {2,0,7,1},
            new int[] {2,0,7,3},
            new int[] {2,0,7,4},
            new int[] {2,0,7,5},
            new int[] {2,0,7,6},
            new int[] {2,1,0,3},
            new int[] {2,1,0,4},
            new int[] {2,1,0,5},
            new int[] {2,1,0,6},
            new int[] {2,1,0,7},
            new int[] {2,1,3,0},
            new int[] {2,1,3,4},
            new int[] {2,1,3,5},
            new int[] {2,1,3,6},
            new int[] {2,1,3,7},
            new int[] {2,1,4,0},
            new int[] {2,1,4,3},
            new int[] {2,1,4,5},
            new int[] {2,1,4,6},
            new int[] {2,1,4,7},
            new int[] {2,1,5,0},
            new int[] {2,1,5,3},
            new int[] {2,1,5,4},
            new int[] {2,1,5,6},
            new int[] {2,1,5,7},
            new int[] {2,1,6,0},
            new int[] {2,1,6,3},
            new int[] {2,1,6,4},
            new int[] {2,1,6,5},
            new int[] {2,1,6,7},
            new int[] {2,1,7,0},
            new int[] {2,1,7,3},
            new int[] {2,1,7,4},
            new int[] {2,1,7,5},
            new int[] {2,1,7,6},
            new int[] {2,3,0,1},
            new int[] {2,3,0,4},
            new int[] {2,3,0,5},
            new int[] {2,3,0,6},
            new int[] {2,3,0,7},
            new int[] {2,3,1,0},
            new int[] {2,3,1,4},
            new int[] {2,3,1,5},
            new int[] {2,3,1,6},
            new int[] {2,3,1,7},
            new int[] {2,3,4,0},
            new int[] {2,3,4,1},
            new int[] {2,3,4,5},
            new int[] {2,3,4,6},
            new int[] {2,3,4,7},
            new int[] {2,3,5,0},
            new int[] {2,3,5,1},
            new int[] {2,3,5,4},
            new int[] {2,3,5,6},
            new int[] {2,3,5,7},
            new int[] {2,3,6,0},
            new int[] {2,3,6,1},
            new int[] {2,3,6,4},
            new int[] {2,3,6,5},
            new int[] {2,3,6,7},
            new int[] {2,3,7,0},
            new int[] {2,3,7,1},
            new int[] {2,3,7,4},
            new int[] {2,3,7,5},
            new int[] {2,3,7,6},
            new int[] {2,4,0,1},
            new int[] {2,4,0,3},
            new int[] {2,4,0,5},
            new int[] {2,4,0,6},
            new int[] {2,4,0,7},
            new int[] {2,4,1,0},
            new int[] {2,4,1,3},
            new int[] {2,4,1,5},
            new int[] {2,4,1,6},
            new int[] {2,4,1,7},
            new int[] {2,4,3,0},
            new int[] {2,4,3,1},
            new int[] {2,4,3,5},
            new int[] {2,4,3,6},
            new int[] {2,4,3,7},
            new int[] {2,4,5,0},
            new int[] {2,4,5,1},
            new int[] {2,4,5,3},
            new int[] {2,4,5,6},
            new int[] {2,4,5,7},
            new int[] {2,4,6,0},
            new int[] {2,4,6,1},
            new int[] {2,4,6,3},
            new int[] {2,4,6,5},
            new int[] {2,4,6,7},
            new int[] {2,4,7,0},
            new int[] {2,4,7,1},
            new int[] {2,4,7,3},
            new int[] {2,4,7,5},
            new int[] {2,4,7,6},
            new int[] {2,5,0,1},
            new int[] {2,5,0,3},
            new int[] {2,5,0,4},
            new int[] {2,5,0,6},
            new int[] {2,5,0,7},
            new int[] {2,5,1,0},
            new int[] {2,5,1,3},
            new int[] {2,5,1,4},
            new int[] {2,5,1,6},
            new int[] {2,5,1,7},
            new int[] {2,5,3,0},
            new int[] {2,5,3,1},
            new int[] {2,5,3,4},
            new int[] {2,5,3,6},
            new int[] {2,5,3,7},
            new int[] {2,5,4,0},
            new int[] {2,5,4,1},
            new int[] {2,5,4,3},
            new int[] {2,5,4,6},
            new int[] {2,5,4,7},
            new int[] {2,5,6,0},
            new int[] {2,5,6,1},
            new int[] {2,5,6,3},
            new int[] {2,5,6,4},
            new int[] {2,5,6,7},
            new int[] {2,5,7,0},
            new int[] {2,5,7,1},
            new int[] {2,5,7,3},
            new int[] {2,5,7,4},
            new int[] {2,5,7,6},
            new int[] {2,6,0,1},
            new int[] {2,6,0,3},
            new int[] {2,6,0,4},
            new int[] {2,6,0,5},
            new int[] {2,6,0,7},
            new int[] {2,6,1,0},
            new int[] {2,6,1,3},
            new int[] {2,6,1,4},
            new int[] {2,6,1,5},
            new int[] {2,6,1,7},
            new int[] {2,6,3,0},
            new int[] {2,6,3,1},
            new int[] {2,6,3,4},
            new int[] {2,6,3,5},
            new int[] {2,6,3,7},
            new int[] {2,6,4,0},
            new int[] {2,6,4,1},
            new int[] {2,6,4,3},
            new int[] {2,6,4,5},
            new int[] {2,6,4,7},
            new int[] {2,6,5,0},
            new int[] {2,6,5,1},
            new int[] {2,6,5,3},
            new int[] {2,6,5,4},
            new int[] {2,6,5,7},
            new int[] {2,6,7,0},
            new int[] {2,6,7,1},
            new int[] {2,6,7,3},
            new int[] {2,6,7,4},
            new int[] {2,6,7,5},
            new int[] {2,7,0,1},
            new int[] {2,7,0,3},
            new int[] {2,7,0,4},
            new int[] {2,7,0,5},
            new int[] {2,7,0,6},
            new int[] {2,7,1,0},
            new int[] {2,7,1,3},
            new int[] {2,7,1,4},
            new int[] {2,7,1,5},
            new int[] {2,7,1,6},
            new int[] {2,7,3,0},
            new int[] {2,7,3,1},
            new int[] {2,7,3,4},
            new int[] {2,7,3,5},
            new int[] {2,7,3,6},
            new int[] {2,7,4,0},
            new int[] {2,7,4,1},
            new int[] {2,7,4,3},
            new int[] {2,7,4,5},
            new int[] {2,7,4,6},
            new int[] {2,7,5,0},
            new int[] {2,7,5,1},
            new int[] {2,7,5,3},
            new int[] {2,7,5,4},
            new int[] {2,7,5,6},
            new int[] {2,7,6,0},
            new int[] {2,7,6,1},
            new int[] {2,7,6,3},
            new int[] {2,7,6,4},
            new int[] {2,7,6,5},
            new int[] {3,0,1,2},
            new int[] {3,0,1,4},
            new int[] {3,0,1,5},
            new int[] {3,0,1,6},
            new int[] {3,0,1,7},
            new int[] {3,0,2,1},
            new int[] {3,0,2,4},
            new int[] {3,0,2,5},
            new int[] {3,0,2,6},
            new int[] {3,0,2,7},
            new int[] {3,0,4,1},
            new int[] {3,0,4,2},
            new int[] {3,0,4,5},
            new int[] {3,0,4,6},
            new int[] {3,0,4,7},
            new int[] {3,0,5,1},
            new int[] {3,0,5,2},
            new int[] {3,0,5,4},
            new int[] {3,0,5,6},
            new int[] {3,0,5,7},
            new int[] {3,0,6,1},
            new int[] {3,0,6,2},
            new int[] {3,0,6,4},
            new int[] {3,0,6,5},
            new int[] {3,0,6,7},
            new int[] {3,0,7,1},
            new int[] {3,0,7,2},
            new int[] {3,0,7,4},
            new int[] {3,0,7,5},
            new int[] {3,0,7,6},
            new int[] {3,1,0,2},
            new int[] {3,1,0,4},
            new int[] {3,1,0,5},
            new int[] {3,1,0,6},
            new int[] {3,1,0,7},
            new int[] {3,1,2,0},
            new int[] {3,1,2,4},
            new int[] {3,1,2,5},
            new int[] {3,1,2,6},
            new int[] {3,1,2,7},
            new int[] {3,1,4,0},
            new int[] {3,1,4,2},
            new int[] {3,1,4,5},
            new int[] {3,1,4,6},
            new int[] {3,1,4,7},
            new int[] {3,1,5,0},
            new int[] {3,1,5,2},
            new int[] {3,1,5,4},
            new int[] {3,1,5,6},
            new int[] {3,1,5,7},
            new int[] {3,1,6,0},
            new int[] {3,1,6,2},
            new int[] {3,1,6,4},
            new int[] {3,1,6,5},
            new int[] {3,1,6,7},
            new int[] {3,1,7,0},
            new int[] {3,1,7,2},
            new int[] {3,1,7,4},
            new int[] {3,1,7,5},
            new int[] {3,1,7,6},
            new int[] {3,2,0,1},
            new int[] {3,2,0,4},
            new int[] {3,2,0,5},
            new int[] {3,2,0,6},
            new int[] {3,2,0,7},
            new int[] {3,2,1,0},
            new int[] {3,2,1,4},
            new int[] {3,2,1,5},
            new int[] {3,2,1,6},
            new int[] {3,2,1,7},
            new int[] {3,2,4,0},
            new int[] {3,2,4,1},
            new int[] {3,2,4,5},
            new int[] {3,2,4,6},
            new int[] {3,2,4,7},
            new int[] {3,2,5,0},
            new int[] {3,2,5,1},
            new int[] {3,2,5,4},
            new int[] {3,2,5,6},
            new int[] {3,2,5,7},
            new int[] {3,2,6,0},
            new int[] {3,2,6,1},
            new int[] {3,2,6,4},
            new int[] {3,2,6,5},
            new int[] {3,2,6,7},
            new int[] {3,2,7,0},
            new int[] {3,2,7,1},
            new int[] {3,2,7,4},
            new int[] {3,2,7,5},
            new int[] {3,2,7,6},
            new int[] {3,4,0,1},
            new int[] {3,4,0,2},
            new int[] {3,4,0,5},
            new int[] {3,4,0,6},
            new int[] {3,4,0,7},
            new int[] {3,4,1,0},
            new int[] {3,4,1,2},
            new int[] {3,4,1,5},
            new int[] {3,4,1,6},
            new int[] {3,4,1,7},
            new int[] {3,4,2,0},
            new int[] {3,4,2,1},
            new int[] {3,4,2,5},
            new int[] {3,4,2,6},
            new int[] {3,4,2,7},
            new int[] {3,4,5,0},
            new int[] {3,4,5,1},
            new int[] {3,4,5,2},
            new int[] {3,4,5,6},
            new int[] {3,4,5,7},
            new int[] {3,4,6,0},
            new int[] {3,4,6,1},
            new int[] {3,4,6,2},
            new int[] {3,4,6,5},
            new int[] {3,4,6,7},
            new int[] {3,4,7,0},
            new int[] {3,4,7,1},
            new int[] {3,4,7,2},
            new int[] {3,4,7,5},
            new int[] {3,4,7,6},
            new int[] {3,5,0,1},
            new int[] {3,5,0,2},
            new int[] {3,5,0,4},
            new int[] {3,5,0,6},
            new int[] {3,5,0,7},
            new int[] {3,5,1,0},
            new int[] {3,5,1,2},
            new int[] {3,5,1,4},
            new int[] {3,5,1,6},
            new int[] {3,5,1,7},
            new int[] {3,5,2,0},
            new int[] {3,5,2,1},
            new int[] {3,5,2,4},
            new int[] {3,5,2,6},
            new int[] {3,5,2,7},
            new int[] {3,5,4,0},
            new int[] {3,5,4,1},
            new int[] {3,5,4,2},
            new int[] {3,5,4,6},
            new int[] {3,5,4,7},
            new int[] {3,5,6,0},
            new int[] {3,5,6,1},
            new int[] {3,5,6,2},
            new int[] {3,5,6,4},
            new int[] {3,5,6,7},
            new int[] {3,5,7,0},
            new int[] {3,5,7,1},
            new int[] {3,5,7,2},
            new int[] {3,5,7,4},
            new int[] {3,5,7,6},
            new int[] {3,6,0,1},
            new int[] {3,6,0,2},
            new int[] {3,6,0,4},
            new int[] {3,6,0,5},
            new int[] {3,6,0,7},
            new int[] {3,6,1,0},
            new int[] {3,6,1,2},
            new int[] {3,6,1,4},
            new int[] {3,6,1,5},
            new int[] {3,6,1,7},
            new int[] {3,6,2,0},
            new int[] {3,6,2,1},
            new int[] {3,6,2,4},
            new int[] {3,6,2,5},
            new int[] {3,6,2,7},
            new int[] {3,6,4,0},
            new int[] {3,6,4,1},
            new int[] {3,6,4,2},
            new int[] {3,6,4,5},
            new int[] {3,6,4,7},
            new int[] {3,6,5,0},
            new int[] {3,6,5,1},
            new int[] {3,6,5,2},
            new int[] {3,6,5,4},
            new int[] {3,6,5,7},
            new int[] {3,6,7,0},
            new int[] {3,6,7,1},
            new int[] {3,6,7,2},
            new int[] {3,6,7,4},
            new int[] {3,6,7,5},
            new int[] {3,7,0,1},
            new int[] {3,7,0,2},
            new int[] {3,7,0,4},
            new int[] {3,7,0,5},
            new int[] {3,7,0,6},
            new int[] {3,7,1,0},
            new int[] {3,7,1,2},
            new int[] {3,7,1,4},
            new int[] {3,7,1,5},
            new int[] {3,7,1,6},
            new int[] {3,7,2,0},
            new int[] {3,7,2,1},
            new int[] {3,7,2,4},
            new int[] {3,7,2,5},
            new int[] {3,7,2,6},
            new int[] {3,7,4,0},
            new int[] {3,7,4,1},
            new int[] {3,7,4,2},
            new int[] {3,7,4,5},
            new int[] {3,7,4,6},
            new int[] {3,7,5,0},
            new int[] {3,7,5,1},
            new int[] {3,7,5,2},
            new int[] {3,7,5,4},
            new int[] {3,7,5,6},
            new int[] {3,7,6,0},
            new int[] {3,7,6,1},
            new int[] {3,7,6,2},
            new int[] {3,7,6,4},
            new int[] {3,7,6,5},
            new int[] {4,0,1,2},
            new int[] {4,0,1,3},
            new int[] {4,0,1,5},
            new int[] {4,0,1,6},
            new int[] {4,0,1,7},
            new int[] {4,0,2,1},
            new int[] {4,0,2,3},
            new int[] {4,0,2,5},
            new int[] {4,0,2,6},
            new int[] {4,0,2,7},
            new int[] {4,0,3,1},
            new int[] {4,0,3,2},
            new int[] {4,0,3,5},
            new int[] {4,0,3,6},
            new int[] {4,0,3,7},
            new int[] {4,0,5,1},
            new int[] {4,0,5,2},
            new int[] {4,0,5,3},
            new int[] {4,0,5,6},
            new int[] {4,0,5,7},
            new int[] {4,0,6,1},
            new int[] {4,0,6,2},
            new int[] {4,0,6,3},
            new int[] {4,0,6,5},
            new int[] {4,0,6,7},
            new int[] {4,0,7,1},
            new int[] {4,0,7,2},
            new int[] {4,0,7,3},
            new int[] {4,0,7,5},
            new int[] {4,0,7,6},
            new int[] {4,1,0,2},
            new int[] {4,1,0,3},
            new int[] {4,1,0,5},
            new int[] {4,1,0,6},
            new int[] {4,1,0,7},
            new int[] {4,1,2,0},
            new int[] {4,1,2,3},
            new int[] {4,1,2,5},
            new int[] {4,1,2,6},
            new int[] {4,1,2,7},
            new int[] {4,1,3,0},
            new int[] {4,1,3,2},
            new int[] {4,1,3,5},
            new int[] {4,1,3,6},
            new int[] {4,1,3,7},
            new int[] {4,1,5,0},
            new int[] {4,1,5,2},
            new int[] {4,1,5,3},
            new int[] {4,1,5,6},
            new int[] {4,1,5,7},
            new int[] {4,1,6,0},
            new int[] {4,1,6,2},
            new int[] {4,1,6,3},
            new int[] {4,1,6,5},
            new int[] {4,1,6,7},
            new int[] {4,1,7,0},
            new int[] {4,1,7,2},
            new int[] {4,1,7,3},
            new int[] {4,1,7,5},
            new int[] {4,1,7,6},
            new int[] {4,2,0,1},
            new int[] {4,2,0,3},
            new int[] {4,2,0,5},
            new int[] {4,2,0,6},
            new int[] {4,2,0,7},
            new int[] {4,2,1,0},
            new int[] {4,2,1,3},
            new int[] {4,2,1,5},
            new int[] {4,2,1,6},
            new int[] {4,2,1,7},
            new int[] {4,2,3,0},
            new int[] {4,2,3,1},
            new int[] {4,2,3,5},
            new int[] {4,2,3,6},
            new int[] {4,2,3,7},
            new int[] {4,2,5,0},
            new int[] {4,2,5,1},
            new int[] {4,2,5,3},
            new int[] {4,2,5,6},
            new int[] {4,2,5,7},
            new int[] {4,2,6,0},
            new int[] {4,2,6,1},
            new int[] {4,2,6,3},
            new int[] {4,2,6,5},
            new int[] {4,2,6,7},
            new int[] {4,2,7,0},
            new int[] {4,2,7,1},
            new int[] {4,2,7,3},
            new int[] {4,2,7,5},
            new int[] {4,2,7,6},
            new int[] {4,3,0,1},
            new int[] {4,3,0,2},
            new int[] {4,3,0,5},
            new int[] {4,3,0,6},
            new int[] {4,3,0,7},
            new int[] {4,3,1,0},
            new int[] {4,3,1,2},
            new int[] {4,3,1,5},
            new int[] {4,3,1,6},
            new int[] {4,3,1,7},
            new int[] {4,3,2,0},
            new int[] {4,3,2,1},
            new int[] {4,3,2,5},
            new int[] {4,3,2,6},
            new int[] {4,3,2,7},
            new int[] {4,3,5,0},
            new int[] {4,3,5,1},
            new int[] {4,3,5,2},
            new int[] {4,3,5,6},
            new int[] {4,3,5,7},
            new int[] {4,3,6,0},
            new int[] {4,3,6,1},
            new int[] {4,3,6,2},
            new int[] {4,3,6,5},
            new int[] {4,3,6,7},
            new int[] {4,3,7,0},
            new int[] {4,3,7,1},
            new int[] {4,3,7,2},
            new int[] {4,3,7,5},
            new int[] {4,3,7,6},
            new int[] {4,5,0,1},
            new int[] {4,5,0,2},
            new int[] {4,5,0,3},
            new int[] {4,5,0,6},
            new int[] {4,5,0,7},
            new int[] {4,5,1,0},
            new int[] {4,5,1,2},
            new int[] {4,5,1,3},
            new int[] {4,5,1,6},
            new int[] {4,5,1,7},
            new int[] {4,5,2,0},
            new int[] {4,5,2,1},
            new int[] {4,5,2,3},
            new int[] {4,5,2,6},
            new int[] {4,5,2,7},
            new int[] {4,5,3,0},
            new int[] {4,5,3,1},
            new int[] {4,5,3,2},
            new int[] {4,5,3,6},
            new int[] {4,5,3,7},
            new int[] {4,5,6,0},
            new int[] {4,5,6,1},
            new int[] {4,5,6,2},
            new int[] {4,5,6,3},
            new int[] {4,5,6,7},
            new int[] {4,5,7,0},
            new int[] {4,5,7,1},
            new int[] {4,5,7,2},
            new int[] {4,5,7,3},
            new int[] {4,5,7,6},
            new int[] {4,6,0,1},
            new int[] {4,6,0,2},
            new int[] {4,6,0,3},
            new int[] {4,6,0,5},
            new int[] {4,6,0,7},
            new int[] {4,6,1,0},
            new int[] {4,6,1,2},
            new int[] {4,6,1,3},
            new int[] {4,6,1,5},
            new int[] {4,6,1,7},
            new int[] {4,6,2,0},
            new int[] {4,6,2,1},
            new int[] {4,6,2,3},
            new int[] {4,6,2,5},
            new int[] {4,6,2,7},
            new int[] {4,6,3,0},
            new int[] {4,6,3,1},
            new int[] {4,6,3,2},
            new int[] {4,6,3,5},
            new int[] {4,6,3,7},
            new int[] {4,6,5,0},
            new int[] {4,6,5,1},
            new int[] {4,6,5,2},
            new int[] {4,6,5,3},
            new int[] {4,6,5,7},
            new int[] {4,6,7,0},
            new int[] {4,6,7,1},
            new int[] {4,6,7,2},
            new int[] {4,6,7,3},
            new int[] {4,6,7,5},
            new int[] {4,7,0,1},
            new int[] {4,7,0,2},
            new int[] {4,7,0,3},
            new int[] {4,7,0,5},
            new int[] {4,7,0,6},
            new int[] {4,7,1,0},
            new int[] {4,7,1,2},
            new int[] {4,7,1,3},
            new int[] {4,7,1,5},
            new int[] {4,7,1,6},
            new int[] {4,7,2,0},
            new int[] {4,7,2,1},
            new int[] {4,7,2,3},
            new int[] {4,7,2,5},
            new int[] {4,7,2,6},
            new int[] {4,7,3,0},
            new int[] {4,7,3,1},
            new int[] {4,7,3,2},
            new int[] {4,7,3,5},
            new int[] {4,7,3,6},
            new int[] {4,7,5,0},
            new int[] {4,7,5,1},
            new int[] {4,7,5,2},
            new int[] {4,7,5,3},
            new int[] {4,7,5,6},
            new int[] {4,7,6,0},
            new int[] {4,7,6,1},
            new int[] {4,7,6,2},
            new int[] {4,7,6,3},
            new int[] {4,7,6,5},
            new int[] {5,0,1,2},
            new int[] {5,0,1,3},
            new int[] {5,0,1,4},
            new int[] {5,0,1,6},
            new int[] {5,0,1,7},
            new int[] {5,0,2,1},
            new int[] {5,0,2,3},
            new int[] {5,0,2,4},
            new int[] {5,0,2,6},
            new int[] {5,0,2,7},
            new int[] {5,0,3,1},
            new int[] {5,0,3,2},
            new int[] {5,0,3,4},
            new int[] {5,0,3,6},
            new int[] {5,0,3,7},
            new int[] {5,0,4,1},
            new int[] {5,0,4,2},
            new int[] {5,0,4,3},
            new int[] {5,0,4,6},
            new int[] {5,0,4,7},
            new int[] {5,0,6,1},
            new int[] {5,0,6,2},
            new int[] {5,0,6,3},
            new int[] {5,0,6,4},
            new int[] {5,0,6,7},
            new int[] {5,0,7,1},
            new int[] {5,0,7,2},
            new int[] {5,0,7,3},
            new int[] {5,0,7,4},
            new int[] {5,0,7,6},
            new int[] {5,1,0,2},
            new int[] {5,1,0,3},
            new int[] {5,1,0,4},
            new int[] {5,1,0,6},
            new int[] {5,1,0,7},
            new int[] {5,1,2,0},
            new int[] {5,1,2,3},
            new int[] {5,1,2,4},
            new int[] {5,1,2,6},
            new int[] {5,1,2,7},
            new int[] {5,1,3,0},
            new int[] {5,1,3,2},
            new int[] {5,1,3,4},
            new int[] {5,1,3,6},
            new int[] {5,1,3,7},
            new int[] {5,1,4,0},
            new int[] {5,1,4,2},
            new int[] {5,1,4,3},
            new int[] {5,1,4,6},
            new int[] {5,1,4,7},
            new int[] {5,1,6,0},
            new int[] {5,1,6,2},
            new int[] {5,1,6,3},
            new int[] {5,1,6,4},
            new int[] {5,1,6,7},
            new int[] {5,1,7,0},
            new int[] {5,1,7,2},
            new int[] {5,1,7,3},
            new int[] {5,1,7,4},
            new int[] {5,1,7,6},
            new int[] {5,2,0,1},
            new int[] {5,2,0,3},
            new int[] {5,2,0,4},
            new int[] {5,2,0,6},
            new int[] {5,2,0,7},
            new int[] {5,2,1,0},
            new int[] {5,2,1,3},
            new int[] {5,2,1,4},
            new int[] {5,2,1,6},
            new int[] {5,2,1,7},
            new int[] {5,2,3,0},
            new int[] {5,2,3,1},
            new int[] {5,2,3,4},
            new int[] {5,2,3,6},
            new int[] {5,2,3,7},
            new int[] {5,2,4,0},
            new int[] {5,2,4,1},
            new int[] {5,2,4,3},
            new int[] {5,2,4,6},
            new int[] {5,2,4,7},
            new int[] {5,2,6,0},
            new int[] {5,2,6,1},
            new int[] {5,2,6,3},
            new int[] {5,2,6,4},
            new int[] {5,2,6,7},
            new int[] {5,2,7,0},
            new int[] {5,2,7,1},
            new int[] {5,2,7,3},
            new int[] {5,2,7,4},
            new int[] {5,2,7,6},
            new int[] {5,3,0,1},
            new int[] {5,3,0,2},
            new int[] {5,3,0,4},
            new int[] {5,3,0,6},
            new int[] {5,3,0,7},
            new int[] {5,3,1,0},
            new int[] {5,3,1,2},
            new int[] {5,3,1,4},
            new int[] {5,3,1,6},
            new int[] {5,3,1,7},
            new int[] {5,3,2,0},
            new int[] {5,3,2,1},
            new int[] {5,3,2,4},
            new int[] {5,3,2,6},
            new int[] {5,3,2,7},
            new int[] {5,3,4,0},
            new int[] {5,3,4,1},
            new int[] {5,3,4,2},
            new int[] {5,3,4,6},
            new int[] {5,3,4,7},
            new int[] {5,3,6,0},
            new int[] {5,3,6,1},
            new int[] {5,3,6,2},
            new int[] {5,3,6,4},
            new int[] {5,3,6,7},
            new int[] {5,3,7,0},
            new int[] {5,3,7,1},
            new int[] {5,3,7,2},
            new int[] {5,3,7,4},
            new int[] {5,3,7,6},
            new int[] {5,4,0,1},
            new int[] {5,4,0,2},
            new int[] {5,4,0,3},
            new int[] {5,4,0,6},
            new int[] {5,4,0,7},
            new int[] {5,4,1,0},
            new int[] {5,4,1,2},
            new int[] {5,4,1,3},
            new int[] {5,4,1,6},
            new int[] {5,4,1,7},
            new int[] {5,4,2,0},
            new int[] {5,4,2,1},
            new int[] {5,4,2,3},
            new int[] {5,4,2,6},
            new int[] {5,4,2,7},
            new int[] {5,4,3,0},
            new int[] {5,4,3,1},
            new int[] {5,4,3,2},
            new int[] {5,4,3,6},
            new int[] {5,4,3,7},
            new int[] {5,4,6,0},
            new int[] {5,4,6,1},
            new int[] {5,4,6,2},
            new int[] {5,4,6,3},
            new int[] {5,4,6,7},
            new int[] {5,4,7,0},
            new int[] {5,4,7,1},
            new int[] {5,4,7,2},
            new int[] {5,4,7,3},
            new int[] {5,4,7,6},
            new int[] {5,6,0,1},
            new int[] {5,6,0,2},
            new int[] {5,6,0,3},
            new int[] {5,6,0,4},
            new int[] {5,6,0,7},
            new int[] {5,6,1,0},
            new int[] {5,6,1,2},
            new int[] {5,6,1,3},
            new int[] {5,6,1,4},
            new int[] {5,6,1,7},
            new int[] {5,6,2,0},
            new int[] {5,6,2,1},
            new int[] {5,6,2,3},
            new int[] {5,6,2,4},
            new int[] {5,6,2,7},
            new int[] {5,6,3,0},
            new int[] {5,6,3,1},
            new int[] {5,6,3,2},
            new int[] {5,6,3,4},
            new int[] {5,6,3,7},
            new int[] {5,6,4,0},
            new int[] {5,6,4,1},
            new int[] {5,6,4,2},
            new int[] {5,6,4,3},
            new int[] {5,6,4,7},
            new int[] {5,6,7,0},
            new int[] {5,6,7,1},
            new int[] {5,6,7,2},
            new int[] {5,6,7,3},
            new int[] {5,6,7,4},
            new int[] {5,7,0,1},
            new int[] {5,7,0,2},
            new int[] {5,7,0,3},
            new int[] {5,7,0,4},
            new int[] {5,7,0,6},
            new int[] {5,7,1,0},
            new int[] {5,7,1,2},
            new int[] {5,7,1,3},
            new int[] {5,7,1,4},
            new int[] {5,7,1,6},
            new int[] {5,7,2,0},
            new int[] {5,7,2,1},
            new int[] {5,7,2,3},
            new int[] {5,7,2,4},
            new int[] {5,7,2,6},
            new int[] {5,7,3,0},
            new int[] {5,7,3,1},
            new int[] {5,7,3,2},
            new int[] {5,7,3,4},
            new int[] {5,7,3,6},
            new int[] {5,7,4,0},
            new int[] {5,7,4,1},
            new int[] {5,7,4,2},
            new int[] {5,7,4,3},
            new int[] {5,7,4,6},
            new int[] {5,7,6,0},
            new int[] {5,7,6,1},
            new int[] {5,7,6,2},
            new int[] {5,7,6,3},
            new int[] {5,7,6,4},
            new int[] {6,0,1,2},
            new int[] {6,0,1,3},
            new int[] {6,0,1,4},
            new int[] {6,0,1,5},
            new int[] {6,0,1,7},
            new int[] {6,0,2,1},
            new int[] {6,0,2,3},
            new int[] {6,0,2,4},
            new int[] {6,0,2,5},
            new int[] {6,0,2,7},
            new int[] {6,0,3,1},
            new int[] {6,0,3,2},
            new int[] {6,0,3,4},
            new int[] {6,0,3,5},
            new int[] {6,0,3,7},
            new int[] {6,0,4,1},
            new int[] {6,0,4,2},
            new int[] {6,0,4,3},
            new int[] {6,0,4,5},
            new int[] {6,0,4,7},
            new int[] {6,0,5,1},
            new int[] {6,0,5,2},
            new int[] {6,0,5,3},
            new int[] {6,0,5,4},
            new int[] {6,0,5,7},
            new int[] {6,0,7,1},
            new int[] {6,0,7,2},
            new int[] {6,0,7,3},
            new int[] {6,0,7,4},
            new int[] {6,0,7,5},
            new int[] {6,1,0,2},
            new int[] {6,1,0,3},
            new int[] {6,1,0,4},
            new int[] {6,1,0,5},
            new int[] {6,1,0,7},
            new int[] {6,1,2,0},
            new int[] {6,1,2,3},
            new int[] {6,1,2,4},
            new int[] {6,1,2,5},
            new int[] {6,1,2,7},
            new int[] {6,1,3,0},
            new int[] {6,1,3,2},
            new int[] {6,1,3,4},
            new int[] {6,1,3,5},
            new int[] {6,1,3,7},
            new int[] {6,1,4,0},
            new int[] {6,1,4,2},
            new int[] {6,1,4,3},
            new int[] {6,1,4,5},
            new int[] {6,1,4,7},
            new int[] {6,1,5,0},
            new int[] {6,1,5,2},
            new int[] {6,1,5,3},
            new int[] {6,1,5,4},
            new int[] {6,1,5,7},
            new int[] {6,1,7,0},
            new int[] {6,1,7,2},
            new int[] {6,1,7,3},
            new int[] {6,1,7,4},
            new int[] {6,1,7,5},
            new int[] {6,2,0,1},
            new int[] {6,2,0,3},
            new int[] {6,2,0,4},
            new int[] {6,2,0,5},
            new int[] {6,2,0,7},
            new int[] {6,2,1,0},
            new int[] {6,2,1,3},
            new int[] {6,2,1,4},
            new int[] {6,2,1,5},
            new int[] {6,2,1,7},
            new int[] {6,2,3,0},
            new int[] {6,2,3,1},
            new int[] {6,2,3,4},
            new int[] {6,2,3,5},
            new int[] {6,2,3,7},
            new int[] {6,2,4,0},
            new int[] {6,2,4,1},
            new int[] {6,2,4,3},
            new int[] {6,2,4,5},
            new int[] {6,2,4,7},
            new int[] {6,2,5,0},
            new int[] {6,2,5,1},
            new int[] {6,2,5,3},
            new int[] {6,2,5,4},
            new int[] {6,2,5,7},
            new int[] {6,2,7,0},
            new int[] {6,2,7,1},
            new int[] {6,2,7,3},
            new int[] {6,2,7,4},
            new int[] {6,2,7,5},
            new int[] {6,3,0,1},
            new int[] {6,3,0,2},
            new int[] {6,3,0,4},
            new int[] {6,3,0,5},
            new int[] {6,3,0,7},
            new int[] {6,3,1,0},
            new int[] {6,3,1,2},
            new int[] {6,3,1,4},
            new int[] {6,3,1,5},
            new int[] {6,3,1,7},
            new int[] {6,3,2,0},
            new int[] {6,3,2,1},
            new int[] {6,3,2,4},
            new int[] {6,3,2,5},
            new int[] {6,3,2,7},
            new int[] {6,3,4,0},
            new int[] {6,3,4,1},
            new int[] {6,3,4,2},
            new int[] {6,3,4,5},
            new int[] {6,3,4,7},
            new int[] {6,3,5,0},
            new int[] {6,3,5,1},
            new int[] {6,3,5,2},
            new int[] {6,3,5,4},
            new int[] {6,3,5,7},
            new int[] {6,3,7,0},
            new int[] {6,3,7,1},
            new int[] {6,3,7,2},
            new int[] {6,3,7,4},
            new int[] {6,3,7,5},
            new int[] {6,4,0,1},
            new int[] {6,4,0,2},
            new int[] {6,4,0,3},
            new int[] {6,4,0,5},
            new int[] {6,4,0,7},
            new int[] {6,4,1,0},
            new int[] {6,4,1,2},
            new int[] {6,4,1,3},
            new int[] {6,4,1,5},
            new int[] {6,4,1,7},
            new int[] {6,4,2,0},
            new int[] {6,4,2,1},
            new int[] {6,4,2,3},
            new int[] {6,4,2,5},
            new int[] {6,4,2,7},
            new int[] {6,4,3,0},
            new int[] {6,4,3,1},
            new int[] {6,4,3,2},
            new int[] {6,4,3,5},
            new int[] {6,4,3,7},
            new int[] {6,4,5,0},
            new int[] {6,4,5,1},
            new int[] {6,4,5,2},
            new int[] {6,4,5,3},
            new int[] {6,4,5,7},
            new int[] {6,4,7,0},
            new int[] {6,4,7,1},
            new int[] {6,4,7,2},
            new int[] {6,4,7,3},
            new int[] {6,4,7,5},
            new int[] {6,5,0,1},
            new int[] {6,5,0,2},
            new int[] {6,5,0,3},
            new int[] {6,5,0,4},
            new int[] {6,5,0,7},
            new int[] {6,5,1,0},
            new int[] {6,5,1,2},
            new int[] {6,5,1,3},
            new int[] {6,5,1,4},
            new int[] {6,5,1,7},
            new int[] {6,5,2,0},
            new int[] {6,5,2,1},
            new int[] {6,5,2,3},
            new int[] {6,5,2,4},
            new int[] {6,5,2,7},
            new int[] {6,5,3,0},
            new int[] {6,5,3,1},
            new int[] {6,5,3,2},
            new int[] {6,5,3,4},
            new int[] {6,5,3,7},
            new int[] {6,5,4,0},
            new int[] {6,5,4,1},
            new int[] {6,5,4,2},
            new int[] {6,5,4,3},
            new int[] {6,5,4,7},
            new int[] {6,5,7,0},
            new int[] {6,5,7,1},
            new int[] {6,5,7,2},
            new int[] {6,5,7,3},
            new int[] {6,5,7,4},
            new int[] {6,7,0,1},
            new int[] {6,7,0,2},
            new int[] {6,7,0,3},
            new int[] {6,7,0,4},
            new int[] {6,7,0,5},
            new int[] {6,7,1,0},
            new int[] {6,7,1,2},
            new int[] {6,7,1,3},
            new int[] {6,7,1,4},
            new int[] {6,7,1,5},
            new int[] {6,7,2,0},
            new int[] {6,7,2,1},
            new int[] {6,7,2,3},
            new int[] {6,7,2,4},
            new int[] {6,7,2,5},
            new int[] {6,7,3,0},
            new int[] {6,7,3,1},
            new int[] {6,7,3,2},
            new int[] {6,7,3,4},
            new int[] {6,7,3,5},
            new int[] {6,7,4,0},
            new int[] {6,7,4,1},
            new int[] {6,7,4,2},
            new int[] {6,7,4,3},
            new int[] {6,7,4,5},
            new int[] {6,7,5,0},
            new int[] {6,7,5,1},
            new int[] {6,7,5,2},
            new int[] {6,7,5,3},
            new int[] {6,7,5,4},
            new int[] {7,0,1,2},
            new int[] {7,0,1,3},
            new int[] {7,0,1,4},
            new int[] {7,0,1,5},
            new int[] {7,0,1,6},
            new int[] {7,0,2,1},
            new int[] {7,0,2,3},
            new int[] {7,0,2,4},
            new int[] {7,0,2,5},
            new int[] {7,0,2,6},
            new int[] {7,0,3,1},
            new int[] {7,0,3,2},
            new int[] {7,0,3,4},
            new int[] {7,0,3,5},
            new int[] {7,0,3,6},
            new int[] {7,0,4,1},
            new int[] {7,0,4,2},
            new int[] {7,0,4,3},
            new int[] {7,0,4,5},
            new int[] {7,0,4,6},
            new int[] {7,0,5,1},
            new int[] {7,0,5,2},
            new int[] {7,0,5,3},
            new int[] {7,0,5,4},
            new int[] {7,0,5,6},
            new int[] {7,0,6,1},
            new int[] {7,0,6,2},
            new int[] {7,0,6,3},
            new int[] {7,0,6,4},
            new int[] {7,0,6,5},
            new int[] {7,1,0,2},
            new int[] {7,1,0,3},
            new int[] {7,1,0,4},
            new int[] {7,1,0,5},
            new int[] {7,1,0,6},
            new int[] {7,1,2,0},
            new int[] {7,1,2,3},
            new int[] {7,1,2,4},
            new int[] {7,1,2,5},
            new int[] {7,1,2,6},
            new int[] {7,1,3,0},
            new int[] {7,1,3,2},
            new int[] {7,1,3,4},
            new int[] {7,1,3,5},
            new int[] {7,1,3,6},
            new int[] {7,1,4,0},
            new int[] {7,1,4,2},
            new int[] {7,1,4,3},
            new int[] {7,1,4,5},
            new int[] {7,1,4,6},
            new int[] {7,1,5,0},
            new int[] {7,1,5,2},
            new int[] {7,1,5,3},
            new int[] {7,1,5,4},
            new int[] {7,1,5,6},
            new int[] {7,1,6,0},
            new int[] {7,1,6,2},
            new int[] {7,1,6,3},
            new int[] {7,1,6,4},
            new int[] {7,1,6,5},
            new int[] {7,2,0,1},
            new int[] {7,2,0,3},
            new int[] {7,2,0,4},
            new int[] {7,2,0,5},
            new int[] {7,2,0,6},
            new int[] {7,2,1,0},
            new int[] {7,2,1,3},
            new int[] {7,2,1,4},
            new int[] {7,2,1,5},
            new int[] {7,2,1,6},
            new int[] {7,2,3,0},
            new int[] {7,2,3,1},
            new int[] {7,2,3,4},
            new int[] {7,2,3,5},
            new int[] {7,2,3,6},
            new int[] {7,2,4,0},
            new int[] {7,2,4,1},
            new int[] {7,2,4,3},
            new int[] {7,2,4,5},
            new int[] {7,2,4,6},
            new int[] {7,2,5,0},
            new int[] {7,2,5,1},
            new int[] {7,2,5,3},
            new int[] {7,2,5,4},
            new int[] {7,2,5,6},
            new int[] {7,2,6,0},
            new int[] {7,2,6,1},
            new int[] {7,2,6,3},
            new int[] {7,2,6,4},
            new int[] {7,2,6,5},
            new int[] {7,3,0,1},
            new int[] {7,3,0,2},
            new int[] {7,3,0,4},
            new int[] {7,3,0,5},
            new int[] {7,3,0,6},
            new int[] {7,3,1,0},
            new int[] {7,3,1,2},
            new int[] {7,3,1,4},
            new int[] {7,3,1,5},
            new int[] {7,3,1,6},
            new int[] {7,3,2,0},
            new int[] {7,3,2,1},
            new int[] {7,3,2,4},
            new int[] {7,3,2,5},
            new int[] {7,3,2,6},
            new int[] {7,3,4,0},
            new int[] {7,3,4,1},
            new int[] {7,3,4,2},
            new int[] {7,3,4,5},
            new int[] {7,3,4,6},
            new int[] {7,3,5,0},
            new int[] {7,3,5,1},
            new int[] {7,3,5,2},
            new int[] {7,3,5,4},
            new int[] {7,3,5,6},
            new int[] {7,3,6,0},
            new int[] {7,3,6,1},
            new int[] {7,3,6,2},
            new int[] {7,3,6,4},
            new int[] {7,3,6,5},
            new int[] {7,4,0,1},
            new int[] {7,4,0,2},
            new int[] {7,4,0,3},
            new int[] {7,4,0,5},
            new int[] {7,4,0,6},
            new int[] {7,4,1,0},
            new int[] {7,4,1,2},
            new int[] {7,4,1,3},
            new int[] {7,4,1,5},
            new int[] {7,4,1,6},
            new int[] {7,4,2,0},
            new int[] {7,4,2,1},
            new int[] {7,4,2,3},
            new int[] {7,4,2,5},
            new int[] {7,4,2,6},
            new int[] {7,4,3,0},
            new int[] {7,4,3,1},
            new int[] {7,4,3,2},
            new int[] {7,4,3,5},
            new int[] {7,4,3,6},
            new int[] {7,4,5,0},
            new int[] {7,4,5,1},
            new int[] {7,4,5,2},
            new int[] {7,4,5,3},
            new int[] {7,4,5,6},
            new int[] {7,4,6,0},
            new int[] {7,4,6,1},
            new int[] {7,4,6,2},
            new int[] {7,4,6,3},
            new int[] {7,4,6,5},
            new int[] {7,5,0,1},
            new int[] {7,5,0,2},
            new int[] {7,5,0,3},
            new int[] {7,5,0,4},
            new int[] {7,5,0,6},
            new int[] {7,5,1,0},
            new int[] {7,5,1,2},
            new int[] {7,5,1,3},
            new int[] {7,5,1,4},
            new int[] {7,5,1,6},
            new int[] {7,5,2,0},
            new int[] {7,5,2,1},
            new int[] {7,5,2,3},
            new int[] {7,5,2,4},
            new int[] {7,5,2,6},
            new int[] {7,5,3,0},
            new int[] {7,5,3,1},
            new int[] {7,5,3,2},
            new int[] {7,5,3,4},
            new int[] {7,5,3,6},
            new int[] {7,5,4,0},
            new int[] {7,5,4,1},
            new int[] {7,5,4,2},
            new int[] {7,5,4,3},
            new int[] {7,5,4,6},
            new int[] {7,5,6,0},
            new int[] {7,5,6,1},
            new int[] {7,5,6,2},
            new int[] {7,5,6,3},
            new int[] {7,5,6,4},
            new int[] {7,6,0,1},
            new int[] {7,6,0,2},
            new int[] {7,6,0,3},
            new int[] {7,6,0,4},
            new int[] {7,6,0,5},
            new int[] {7,6,1,0},
            new int[] {7,6,1,2},
            new int[] {7,6,1,3},
            new int[] {7,6,1,4},
            new int[] {7,6,1,5},
            new int[] {7,6,2,0},
            new int[] {7,6,2,1},
            new int[] {7,6,2,3},
            new int[] {7,6,2,4},
            new int[] {7,6,2,5},
            new int[] {7,6,3,0},
            new int[] {7,6,3,1},
            new int[] {7,6,3,2},
            new int[] {7,6,3,4},
            new int[] {7,6,3,5},
            new int[] {7,6,4,0},
            new int[] {7,6,4,1},
            new int[] {7,6,4,2},
            new int[] {7,6,4,3},
            new int[] {7,6,4,5},
            new int[] {7,6,5,0},
            new int[] {7,6,5,1},
            new int[] {7,6,5,2},
            new int[] {7,6,5,3},
            new int[] {7,6,5,4}
};

   void Awake() {
       if (instance == null) {
           instance = this;
       } else if (instance != this) {
           Destroy(gameObject);
       }
   }

   void Start()
   {
      currReachedCombo = 0;
      currDifficulty = 0;
      seqPlayed = 0;
      realTimer = 0;
      microTimer = 0;
      symbol0.SetActive(false);
      symbol1.SetActive(false);
      symbol2.SetActive(false);
      symbol3.SetActive(false);
      symbol4.SetActive(false);
      symbol5.SetActive(false);
      symbol6.SetActive(false);
      symbol7.SetActive(false);
      comboCntr = 0;
      bonusPts = 1;

      currBeatSeq = diff0[UnityEngine.Random.Range(0,diff0.GetLength(0))];//###ToEdit
      Debug.Log(currBeatSeq[0] + "," + currBeatSeq[1] + "," + currBeatSeq[2] + "," + currBeatSeq[3]);

      beatCounter = 0;
      theSong.Play();
      setAccDifficulty(1);
      startDancing = false;
      hasMatched = false;
      resetMicro = false;
      microStep1 = false;
      microStep2 = false;
      microStep3 = false;
      currPoints = 0;
      preparationImage.sprite = pic1;
      startTime = DateTime.Now;
   }

   void Update()
   {
      if (!theSong.isPlaying)
      { //if the song is finished - start it again, reset realTimer, and prepare to reset microTimer
         theSong.Play();
         if (startDancing) startTime = DateTime.Now; //realTimer will not be reset the first time the song finishes
         resetMicro = true;
      }
      realTimer = (DateTime.Now - startTime).Seconds * 1000 + (DateTime.Now - startTime).Milliseconds; //calculate realTimer

      if (!startDancing)
      { //before the game starts

         if (realTimer > offsetMillsec + 2000f)
         { //if the song has played once and the time of the offset has passed begin playing
            startDancing = true;
            danceRequest(currBeatSeq[beatCounter]);
            instructMode = true;
            preparationImage.enabled = false;
            microStartTime = DateTime.Now;
            resetMicro = false;
         }
         else if (realTimer > offsetMillsec + 1400f)
         { //change sign
             preparationImage.sprite = pic4;
         }
         else if (realTimer > offsetMillsec + 800f)
         { //change sign
             preparationImage.sprite = pic3;
         }
         else if (realTimer > offsetMillsec + 200f)
         { //change sign
            preparationImage.sprite = pic2;
         }

      }
      else
      {

         if (resetMicro && realTimer > offsetMillsec)
         { //if the song has reached the point for the new button check - reset the microTimer,
            //check if no matched on the fourth part to display miss, reset all steps
            //and change to next request
            microStartTime = DateTime.Now;
            microStep1 = false;
            microStep2 = false;
            microStep3 = false;
            resetMicro = false;
            if (!instructMode && !hasMatched)
            {
               SpawnIndicator("Miss");
               comboCntr = 0;
               bonusPts = 1;
               if (currPoints < 0) currPoints = 0;
               theScore.text = "Score : " + currPoints;
            }
            hasMatched = false;
            instructMode = !instructMode;
            if (instructMode)
            {
               theFrame.color = new Color(255f, 255f, 255f, theFrame.color.a); 
               seqPlayed++;
               for (int j = 0; j < sequencesPerDifficulty.Count; j++) {
                   if (seqPlayed == sequencesPerDifficulty[j]) {
                       currDifficulty = j; break;
                   }
               }
               nextSeq();
               danceRequest(currBeatSeq[beatCounter]);
               Debug.Log("seqPlayed " + seqPlayed + " Curr Diff: " + currDifficulty);
               Debug.Log("Sequence: " + currBeatSeq[0] + " " + currBeatSeq[1] + " " + currBeatSeq[2] + " " + currBeatSeq[3]);
            } else {
                theFrame.color = new Color(255f, 255f, 0f, theFrame.color.a); 
                if (currentlyVisible) currentlyVisible.SetActive(false);
                currentlyVisible = null;
            }
            beatCounter = 0;
         }

         microTimer = (DateTime.Now - microStartTime).Seconds * 1000 + (DateTime.Now - microStartTime).Milliseconds; //calculate microtimer

         if (microTimer % 500 >= 150 && microTimer % 500 < 250)
         { //light up the frame during the perfect time
            theFrame.color = new Color(theFrame.color.r, theFrame.color.g, theFrame.color.b, .5f);
         }
         else
         {
            theFrame.color = new Color(theFrame.color.r, theFrame.color.g, theFrame.color.b, .1f);
         }

         if (instructMode)
         {
            if (microTimer > 3 * timePerBeat && !microStep3)
            {
               microStep3 = true;
               danceRequest(currBeatSeq[beatCounter]);
               beatCounter = 3;
            }
            else if (microTimer > 2 * timePerBeat && !microStep2)
            {
               microStep2 = true;
               danceRequest(currBeatSeq[beatCounter]);
               beatCounter = 2;
            }
            else if (microTimer > timePerBeat && !microStep1)
            {
               microStep1 = true;
               danceRequest(currBeatSeq[beatCounter]);
               beatCounter = 1;
            }

         }
         else
         {
             if ((Input.GetButtonDown("DPLeft") || Input.GetKeyDown(KeyCode.A)) && currBeatSeq[beatCounter] == 0 && !hasMatched)
            { //check if L is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
             if ((Input.GetButtonDown("DPRight") || Input.GetKeyDown(KeyCode.D)) && currBeatSeq[beatCounter] == 1 && !hasMatched)
            { //check if R is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
             if ((Input.GetButtonDown("DPUp") || Input.GetKeyDown(KeyCode.W)) && currBeatSeq[beatCounter] == 2 && !hasMatched)
            { //check if U is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if ((Input.GetButtonDown("DPDown") || Input.GetKeyDown(KeyCode.X)) && currBeatSeq[beatCounter] == 3 && !hasMatched)
            { //check if D is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if ((Input.GetButtonDown("DPX") || Input.GetKeyDown(KeyCode.Q)) && currBeatSeq[beatCounter] == 4 && !hasMatched)
            { //check if X is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if ((Input.GetButtonDown("DPO") || Input.GetKeyDown(KeyCode.E)) && currBeatSeq[beatCounter] == 5 && !hasMatched)
            { //check if O is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if ((Input.GetButtonDown("DPS") || Input.GetKeyDown(KeyCode.C)) && currBeatSeq[beatCounter] == 6 && !hasMatched)
            { //check if S is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if ((Input.GetButtonDown("DPT") || Input.GetKeyDown(KeyCode.Z)) && currBeatSeq[beatCounter] == 7 && !hasMatched)
            { //check if T is pressed, is correct & not double counted
                hasMatched = true;
                checkAccuracy(microTimer);
            }
            if (currBeatSeq[beatCounter] == 8 && !hasMatched) {
                hasMatched = true;
            }

            if (microTimer > 3 * timePerBeat && !microStep3)
            { //if the third step has not passed and the timer has just passed 1500msc
               microStep3 = true;
               if (!hasMatched)
               {
                   SpawnIndicator("Miss");
                   comboCntr = 0;
                   bonusPts = 1;
                  if (currPoints < 0) currPoints = 0;
                  theScore.text = "Score : " + currPoints;
               }
               beatCounter = 3;
               hasMatched = false;
            }
            else if (microTimer > 2 * timePerBeat && !microStep2)
            { //if the second step has not passed and the timer has just passed 1000msc
               microStep2 = true;
               if (!hasMatched)
               {

                   SpawnIndicator("Miss");
                   comboCntr = 0;
                   bonusPts = 1;
                  if (currPoints < 0) currPoints = 0;
                  theScore.text = "Score : " + currPoints;
               }
               beatCounter = 2;
               hasMatched = false;
            }
            else if (microTimer > timePerBeat && !microStep1)
            { //if the first step has not passed and the timer has just passed 500msc
               microStep1 = true;
               if (!hasMatched)
               {
                   SpawnIndicator("Miss");
                   comboCntr = 0;
                   bonusPts = 1;
                  if (currPoints < 0) currPoints = 0;
                  theScore.text = "Score : " + currPoints;
               }
               beatCounter = 1;
               hasMatched = false;
            }
         }

      }
   }

   void checkAccuracy(float milisecs)
   { //checks hit accuracy and updates score
       milisecs = microTimer % 500;
       comboCntr++;
       for (int i = 0; i < comboBonusList.Count; i++) {
           if (comboCntr > comboBonusList[i].x) {
               bonusPts = (int) comboBonusList[i].y;
               if (i > currReachedCombo) {
                   currReachedCombo = i;
                   switch (i) {
                       case 0: combo1.Play(); break;
                       case 1: combo2.Play(); break;
                       case 2: combo3.Play(); break;
                       case 3: combo4.Play(); break;
                    }
               }
           } else if (comboCntr < comboBonusList[i].x) {
               break;
           }
       }
       if (milisecs < goodHitx2)
      {
         SpawnIndicator("Good");
         currPoints += 1 * bonusPts;
         theScore.text = "Score : " + currPoints;
      } else if (milisecs < goodHitx2 + perfectHit)
      {
         SpawnIndicator("Perfect");
         currPoints += 3 * bonusPts;
         theScore.text = "Score : " + currPoints;
      } else if (milisecs < goodHitx2 * 2 + perfectHit)
      {
         SpawnIndicator("Good");
         currPoints += 1 * bonusPts;
         theScore.text = "Score : " + currPoints;
      } else
      {
         SpawnIndicator("Bad");
         comboCntr = 0;
         bonusPts = 1;
      }
   }

   private static void SpawnIndicator(string prefabName)
   {
      var indicator = Instantiate(Resources.Load(@"Prefabs/" + prefabName)) as GameObject;
      indicator.transform.position = new Vector2(-0.44f, -3.21f);
   }

   void nextSeq()
   {
       switch (currDifficulty) {
           case 0: currBeatSeq = diff0[UnityEngine.Random.Range(0, diff0.GetLength(0))]; break;
           case 1: currBeatSeq = diff1[UnityEngine.Random.Range(0, diff1.GetLength(0))]; break;
           case 2: currBeatSeq = diff2[UnityEngine.Random.Range(0, diff2.GetLength(0))]; break;
           case 3: currBeatSeq = diff3[UnityEngine.Random.Range(0, diff3.GetLength(0))]; break;
           case 4: currBeatSeq = diff4[UnityEngine.Random.Range(0, diff4.GetLength(0))]; break;
           case 5: currBeatSeq = diff5[UnityEngine.Random.Range(0, diff5.GetLength(0))]; break;
           case 6: currBeatSeq = diff6[UnityEngine.Random.Range(0, diff6.GetLength(0))]; break;
           case 7: currBeatSeq = diff7[UnityEngine.Random.Range(0, diff7.GetLength(0))]; break;
           case 8: currBeatSeq = diff8[UnityEngine.Random.Range(0, diff8.GetLength(0))]; break;
           case 9: currBeatSeq = diff9[UnityEngine.Random.Range(0, diff9.GetLength(0))]; break;
           case 10: currBeatSeq = diff10[UnityEngine.Random.Range(0, diff10.GetLength(0))]; break;
           case 11: currBeatSeq = diff11[UnityEngine.Random.Range(0, diff11.GetLength(0))]; break;
           default: currBeatSeq = diff11[UnityEngine.Random.Range(0, diff11.GetLength(0))]; break;
       }     
      beatCounter = 0;
   }

   private void danceRequest(int numb)
   {  switch (numb) {
           case 0: symbol0.SetActive(true); StartCoroutine(lightUpTile(symbol0)); break;
           case 1: symbol1.SetActive(true); StartCoroutine(lightUpTile(symbol1)); break;
           case 2: symbol2.SetActive(true); StartCoroutine(lightUpTile(symbol2)); break;
           case 3: symbol3.SetActive(true); StartCoroutine(lightUpTile(symbol3)); break;
           case 4: symbol4.SetActive(true); StartCoroutine(lightUpTile(symbol4)); break;
           case 5: symbol5.SetActive(true); StartCoroutine(lightUpTile(symbol5)); break;
           case 6: symbol6.SetActive(true); StartCoroutine(lightUpTile(symbol6)); break;
           case 7: symbol7.SetActive(true); StartCoroutine(lightUpTile(symbol7)); break;
           default: break;
      }
   }

   private IEnumerator lightUpTile(GameObject lightPic) {
       yield return new WaitForSeconds(0.19f);
       lightPic.SetActive(false);
   }

   void setAccDifficulty(int diff)
   { //set values based on difficulty, !NB The sequence is goodHitx2, perfectHit, goodHitx2, badHit. They all equal timePerBeat
      switch (diff)
      {
         default:
            goodHitx2 = 100f;
            perfectHit = 200f;
            timePerBeat = 500f;
            break;
      }
   }
}
