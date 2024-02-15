using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket_IsEmpty : Requirements {
    [SerializeField] Bucket bucket;

    public override bool CheckReq() { return bucket.IsEmpty(); }
}
