const fs = require('fs');
const path = require('path');

// 主函数相当于Go的main函数
async function main() {
  
  let p=[{x:0,y:0,z:0}];
  for(let i = 0; i < 100000; i++) {
    let {x,y,z}=p[i];
    p.push({x:x+(Math.random()/100),y:y+(Math.random()/100),z:z+(Math.random()/100)});
  }

  let dataS=JSON.stringify(p);

  fs.writeFileSync("./path.json",dataS);

}

// 调用主函数
main()
  .catch(err => {
    console.error('An error occurred:', err);
  });