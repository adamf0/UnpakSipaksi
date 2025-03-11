import sinta
from fastapi import FastAPI, Request
from fastapi.responses import JSONResponse

app = FastAPI()

@app.get('/api/infoSinta/{author_id}', response_class=JSONResponse)
async def edit(author_id: int, request: Request):
    try:
        author = sinta.author(author_id)
        return JSONResponse(content={"status": 200,"data":author,"input":author_id,"error":""}, status_code=200)
    except ValueError:
        return JSONResponse(content={"status": 500,"data":[],"error":"Invalid author_id"}, status_code=200)