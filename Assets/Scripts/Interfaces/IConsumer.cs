using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumer
{
    void SubsribeConsumer();
    void Consume(Resource resource);
}
