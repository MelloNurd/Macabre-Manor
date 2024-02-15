using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket_IsFull : Requirements {
    [SerializeField] Bucket bucket;

    public override bool CheckReq() { return bucket.IsFull(); }
}
