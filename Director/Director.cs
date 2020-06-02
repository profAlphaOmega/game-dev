using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director : MonoBehaviour
    {

        public static Director instance;
        
        public static Director GetInstance()
	    {
		    return instance;
	    }
	}