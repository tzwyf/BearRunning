using System.Collections;
using System.Collections.Generic;

public interface IReusable {
    //取出时调用
    void OnSpawn();

    //回收时调用
    void OnUnSpawn();
}
