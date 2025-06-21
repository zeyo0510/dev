globalThis._          = globalThis._ || {}
globalThis._.Settings = async function(name)
{
  let response = await fetch(name);
  /************************************************/
  let json = await response.json();
  /************************************************/
  let data = json;
  /************************************************/
  return {
    data: data
  }
}